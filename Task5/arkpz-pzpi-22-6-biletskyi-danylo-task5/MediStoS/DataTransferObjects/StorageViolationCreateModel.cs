using System.Text.Json.Serialization;

namespace MediStoS.DataTransferObjects;

public class StorageViolationCreateModel
{
    [JsonPropertyName("temperature")]
    public float Temperature { get; set; }
    [JsonPropertyName("humidity")]
    public float Humidity { get; set; }
    [JsonPropertyName("recorded_at")]
    public DateTime RecordedAt { get; set; }
    [JsonPropertyName("warehouse_id")]
    public int WarehouseId { get; set; }
}
