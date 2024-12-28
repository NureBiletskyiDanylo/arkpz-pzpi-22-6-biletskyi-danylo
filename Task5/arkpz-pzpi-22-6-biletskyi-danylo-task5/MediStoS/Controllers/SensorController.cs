namespace MediStoS.Controllers;

using MediStoS.Database.Models;
using MediStoS.Database.Repository.SensorRepository;
using MediStoS.Database.Repository.StorageViolationRepository;
using MediStoS.Database.Repository.UserRepository;
using MediStoS.Database.Repository.WarehouseRepository;
using MediStoS.DataTransferObjects;
using MediStoS.Services;
using MediStoS.Services.SMTPService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

[Route("api/[controller]")]
[ApiController]
public class SensorController : ControllerBase
{
    private readonly RelativeHumidityCalculationService _humidityCalculationService = new RelativeHumidityCalculationService();
    private readonly StorageRequirementsChecker _checker;

    private ISensorRepository sensorRepository;
    public SensorController(
        ISensorRepository sensorRepository,
        IWarehouseRepository warehouseRepository,
        IStorageViolationRepository storageViolationRepository,
        IUserRepository userRepository,
        IEmailSender emailSender)
    {
        _checker = new StorageRequirementsChecker(sensorRepository,
            warehouseRepository,
            storageViolationRepository,
            userRepository,
            emailSender
            );
        this.sensorRepository = sensorRepository;
    }
    /// <summary>
    /// Handles a request of getting a sensor by its id 
    /// </summary>
    /// <param name="id">Sensor id<see cref="int"/></param>
    /// <returns>Ok with a sensor object, or an error:<see cref="Task{IActionResult}"/></returns>
    [HttpGet]
    [Authorize]
    [Route("{id}")]
    [SwaggerOperation("Get sensor object by it's id")]
    [ProducesResponseType(typeof(Sensor), 200)]
    public async Task<IActionResult> GetSensor(int id)
    {
        Sensor? sensor = await sensorRepository.GetSensor(id);
        if (sensor == null)
        {
            return NotFound($"Sensor by id {id} was not found");
        }

        return Ok(sensor);
    }

    /// <summary>
    /// Handles a request of creating a new sensor
    /// </summary>
    /// <param name="model">Sensor model<see cref="SensorCreateModel"/></param>
    /// <returns>Ok or an error:<see cref="Task{IActionResult}"/></returns>
    [HttpPost]
    [Route("")]
    [Authorize(Roles = "Manager,Admin,DBAdmin")]
    [SwaggerOperation("Create sensor object")]
    public async Task<IActionResult> AddSensor([FromBody] SensorCreateModel model)
    {
        if (model == null)
        {
            return BadRequest($"Sensor was not added, because data in request body was corrupted");
        }

        if (!await sensorRepository.IsWarehouseExist(model.WarehouseId))
        {
            return NotFound("Can't create a sensor in a warehouse that doesn't exist");
        }

        Sensor sensor = new Sensor(model);
        bool result = await sensorRepository.AddSensor(sensor);
        if (!result) return BadRequest("Sensor was not added");
        return Ok();
    }

    /// <summary>
    /// Handles a request of deleting a sensor by its id
    /// </summary>
    /// <param name="id">Sensor id<see cref="int"/></param>
    /// <returns>Ok or an error:<see cref="Task{IActionResult}"/></returns>
    [HttpDelete]
    [Route("{id}")]
    [Authorize(Roles = "Manager,Admin,DBAdmin")]
    [SwaggerOperation("Delete sensor object by it's id")]
    public async Task<IActionResult> DeleteSensor(int id)
    {
        Sensor? sensor = await sensorRepository.GetSensor(id);
        if (sensor == null)
        {
            return NotFound($"Sensor by id {id} was not found");
        }

        bool result = await sensorRepository.DeleteSensor(sensor);
        if (!result) return BadRequest("Sensor was not deleted");
        return Ok();
    }

    /// <summary>
    /// Handles a request of updating a sensor with new data by its id
    /// </summary>
    /// <param name="model">Sensor new model<see cref="SensorCreateModel"/></param>
    /// <param name="id">Sensor id<see cref="int"/></param>
    /// <returns>Ok with sensor updated object, or an error:<see cref="Task{IActionResult}"/></returns>
    [HttpPut]
    [Route("{id}")]
    [Authorize(Roles = "Manager,Admin,DBAdmin")]
    [SwaggerOperation("Update sensor object specified by it's id")]
    [ProducesResponseType(typeof(Sensor), 200)]
    public async Task<IActionResult> UpdateSensor([FromBody] SensorCreateModel model, int id)
    {
        if (model == null)
        {
            return BadRequest("Coudln't get updated sensor. Data was corrupted");
        }

        Sensor? oldSensor = await sensorRepository.GetSensor(id, false);
        if (oldSensor == null)
        {
            return NotFound($"Sensor by the specified Id : {id} was not found");
        }

        bool isWarehouseExist = await sensorRepository.IsWarehouseExist(model.WarehouseId);
        if (!isWarehouseExist)
        {
            return BadRequest("Updated sensor points at a warehouse that no longer exists");
        }

        Sensor newSensor = new Sensor(model);
        newSensor.Id = id;
        bool result = await sensorRepository.UpdateSensor(newSensor);
        if (!result) return BadRequest("Sensor was not updated");
        return Ok(newSensor);
    }

    /// <summary>
    /// Handles a request of updating a sensor measuring value with new data by its id
    /// </summary>
    /// <param name="model">Sensor updated measuring value<see cref="SensorCreateModel"/></param>
    /// <param name="id">Sensor id<see cref="int"/></param>
    /// <returns>Ok with sensor updated object, or an error:<see cref="Task{IActionResult}"/></returns>
    [HttpPatch]
    [Route("UpdateMeasuredData")]
    [SwaggerOperation("Update measuring value of sensor object specified by it's id")]
    [ProducesResponseType(typeof(Sensor), 200)]
    public async Task<IActionResult> UpdateSensor([FromBody] SensorUpdateValueModel model)
    {
        if (model == null)
        {
            return BadRequest("Coudln't get updated sensor. Data was corrupted");
        }

        Sensor? specifiedSensor = await sensorRepository.GetSensor(model.Id, true);
        if (specifiedSensor == null)
        {
            return NotFound($"Sensor by the specified Id : {model.Id} was not found");
        }

        if (model.Id != specifiedSensor.Id)
        {
            return BadRequest("Attempt to update the sensor by the value of another sensor!");
        }
        
        if (model.Type == Enums.SensorType.Humidity)
        {
            List<Sensor> sensors = await sensorRepository
                .GetSensorsByWarehouseId(specifiedSensor.WarehouseId);
            var sensorsWithTemp = sensors.Where(a => a.Type == Enums.SensorType.Temperature).ToList();
            if (sensorsWithTemp.Count() == 0) 
                return BadRequest("Can't measure relative humidity, as there are " +
                    "no temperature sensors in the warehouse");

            var relativeHumidity = CalculateRelativeHumidity(sensorsWithTemp, model.Value);
            if (relativeHumidity > 100) relativeHumidity = 100;

            specifiedSensor.Value = relativeHumidity;
        }
        else
        {
            specifiedSensor.Value = model.Value;
        }

        bool result = await sensorRepository.UpdateSensor(specifiedSensor);
        if (!result) return BadRequest("It is impossible to set a value to a sensor now!");
        await _checker.StorageRequirementsValidation(specifiedSensor.Id, specifiedSensor.WarehouseId);
        return Ok(specifiedSensor);
    }

    /// <summary>
    /// Handles a request of getting sensors in a warehouse by warehouse id 
    /// </summary>
    /// <param name="warehouseId">Warehouse id<see cref="int"/></param>
    /// <returns>Ok and a list of sensors<see cref="Task{IActionResult}"/></returns>
    [HttpGet]
    [Route("{warehouseId}/Sensors")]
    [Authorize]
    [SwaggerOperation("Get all sensor objects in a warehouse specified by it's id")]
    [ProducesResponseType(typeof(List<Sensor>), 200)]
    public async Task<IActionResult> GetSensors(int warehouseId)
    {
        return Ok(await sensorRepository.GetSensorsByWarehouseId(warehouseId));
    }

    private float CalculateRelativeHumidity(List<Sensor> sensors, float absoluteHumidity)
    {
        float averageTemperature = 0;
        foreach (var sensor in sensors)
        {
            averageTemperature += sensor.Value;
        }

        averageTemperature = averageTemperature / sensors.Count();

        var calculatedRelativeHumidity = _humidityCalculationService
            .CalculateRelativeHumidity(averageTemperature, absoluteHumidity);
        return calculatedRelativeHumidity;

    }

    [HttpGet]
    [Route("{warehouseId}/GetSensorsForIoT")]
    public async Task<IActionResult> GetSensorsForIoT(int warehouseId)
    {
        List<Sensor> sensors = await sensorRepository.GetSensorsByWarehouseId(warehouseId);
        List<SensorsToIoTSend> response = sensors.Select(x => new SensorsToIoTSend(x)).ToList();
        return Ok(response);
    }
}
