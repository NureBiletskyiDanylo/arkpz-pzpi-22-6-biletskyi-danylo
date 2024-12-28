using MediStoS.Database.Models;
using MediStoS.Database.Repository.SensorRepository;
using MediStoS.Database.Repository.StorageViolationRepository;
using MediStoS.Database.Repository.UserRepository;
using MediStoS.Database.Repository.WarehouseRepository;
using MediStoS.Enums;
using MediStoS.Services.SMTPService;

namespace MediStoS.Services;

public class StorageRequirementsChecker(ISensorRepository sensorRepository, 
    IWarehouseRepository warehouseRepository, 
    IStorageViolationRepository storageViolationRepository, 
    IUserRepository userRepository,
    IEmailSender emailSender)
{
    public async Task StorageRequirementsValidation(int sensorId, int warehouseId)
    {
        var sensors = await sensorRepository.GetSensorsByWarehouseId(warehouseId);
        Sensor sensor = sensors.FirstOrDefault(a => a.Id == sensorId)!;
        var warehouse = await warehouseRepository.GetWarehouse(warehouseId, false);
        bool exceeds = false;
        SensorType typeToCheck = SensorType.Temperature;
        if (sensor.Type == Enums.SensorType.Temperature)
        {
            exceeds = sensor.Value > warehouse.MaxTemperature || sensor.Value < warehouse.MinTemperature;
            typeToCheck = SensorType.Humidity;
        }
        else if (sensor.Type == Enums.SensorType.Humidity)
        {
            exceeds = sensor.Value > warehouse.MaxHumidity || sensor.Value < warehouse.MinHumidity;
            typeToCheck = SensorType.Temperature;
        }

        if (exceeds)
        {
            var otherTypeSensorValues = sensors.Where(a => a.Type == typeToCheck).Select(a => a.Value).ToList();
            var average = CalculateAverage(otherTypeSensorValues);
            StorageViolation violation = new StorageViolation()
            {
                RecordedAt = DateTime.Now,
                WarehouseId = warehouseId,
            };

            violation.Temperature = typeToCheck != SensorType.Temperature ? sensor.Value : average;
            violation.Humidity = typeToCheck != SensorType.Humidity ? sensor.Value : average;

            bool isSuccess = await storageViolationRepository.AddStorageViolation(violation);
            violation.Warehouse = warehouse;
            WriteMessage(violation, sensor.Type);
        }
    }

    private float CalculateAverage(List<float> values)
    {
        return values.Average();
    }

    private async void WriteMessage(StorageViolation violation, SensorType violationType)
    {
        List<string> emails = userRepository.GetUsers().Result.Select(a => a.Email).ToList();
        foreach(var email in emails)
        {
            await emailSender.SendEmaiAsync(email: email, subject: $"Storage {violationType.ToString()} violation!", message: $"There has been a violation in warehouse {violation.Warehouse.Name}\n" +
                $"Violation type is: {violationType.ToString()};\n" +
                $"Temperature is: {violation.Temperature};\n" +
                $"Humidity is: {violation.Humidity};\n" +
                $"Recorded at: {violation.RecordedAt.ToString()}");
        }
    }
}
