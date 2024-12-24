namespace MediStoS.Controllers;

using MediStoS.Database.Models;
using MediStoS.Database.Repository.SensorRepository;
using MediStoS.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

[Route("api/[controller]")]
[ApiController]
public class SensorController(ISensorRepository sensorRepository) : ControllerBase
{
    /// <summary>
    /// Handles a request of getting a sensor by its id 
    /// </summary>
    /// <param name="id">Sensor id<see cref="int"/></param>
    /// <returns>Ok with a sensor object, or an error:<see cref="Task{IActionResult}"/></returns>
    [HttpGet]
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
    [SwaggerOperation("Delete sensor object specified by it's id")]
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
        if (!result) return BadRequest("Sensor was not deleted");
        return Ok(newSensor);
    }

    /// <summary>
    /// Handles a request of getting sensors in a warehouse by warehouse id 
    /// </summary>
    /// <param name="warehouseId">Warehouse id<see cref="int"/></param>
    /// <returns>Ok and a list of sensors<see cref="Task{IActionResult}"/></returns>
    [HttpGet]
    [Route("{warehouseId}/Sensors")]
    [SwaggerOperation("Get all sensor objects in a warehouse specified by it's id")]
    [ProducesResponseType(typeof(List<Sensor>), 200)]
    public async Task<IActionResult> GetSensors(int warehouseId)
    {
        return Ok(await sensorRepository.GetSensorsByWarehouseId(warehouseId));
    }
}
