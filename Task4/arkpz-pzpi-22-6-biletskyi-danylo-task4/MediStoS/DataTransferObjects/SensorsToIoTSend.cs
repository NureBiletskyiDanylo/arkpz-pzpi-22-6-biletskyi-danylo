using MediStoS.Database.Models;
using MediStoS.Enums;
using System.Text.Json.Serialization;

namespace MediStoS.DataTransferObjects;

public class SensorsToIoTSend
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("type")]
    public SensorType Type { get; set; }

    public SensorsToIoTSend(Sensor sensor)
    {
        Id = sensor.Id;
        Type = sensor.Type;
    }

}
