using MediStoS.DataTransferObjects;
using MediStoS.Enums;
using System.Text.Json.Serialization;

namespace MediStoS.Database.Models;

public class Sensor
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("type")]
    public SensorType Type { get; set; }
    [JsonPropertyName("serial_number")]
    public string SerialNumber { get; set; } = string.Empty;
    [JsonPropertyName("value")]
    public float Value { get; set; } 
    [JsonPropertyName("warehouse_id")]
    public int WarehouseId { get; set; }
    [JsonIgnore]
    public Warehouse Warehouse { get; set; }

    public Sensor()
    {
    }

    public Sensor(SensorCreateModel model)
    {
        Type = model.Type;
        SerialNumber = model.SerialNumber;
        
        WarehouseId = model.WarehouseId;
    }

    public Sensor(SensorUpdateValueModel model)
    {
        Value = model.Value;
    }
}
