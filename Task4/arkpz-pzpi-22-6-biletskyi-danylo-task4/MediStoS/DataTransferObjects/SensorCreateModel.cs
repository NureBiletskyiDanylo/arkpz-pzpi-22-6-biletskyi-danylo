using MediStoS.Enums;
using System.Text.Json.Serialization;

namespace MediStoS.DataTransferObjects;

public class SensorCreateModel
{
    [JsonPropertyName("type")]
    public SensorType Type { get; set; }
    [JsonPropertyName("serial_number")]
    public string SerialNumber { get; set; } = string.Empty;
    [JsonPropertyName("warehouse_id")]
    public int WarehouseId { get; set; }
}
