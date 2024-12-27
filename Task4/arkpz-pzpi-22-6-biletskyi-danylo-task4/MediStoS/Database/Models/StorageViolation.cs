using MediStoS.DataTransferObjects;
using System.Text.Json.Serialization;

namespace MediStoS.Database.Models;

public class StorageViolation
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("temperature")]
    public float Temperature { get; set; }
    [JsonPropertyName("humidity")]
    public float Humidity { get; set; }
    [JsonPropertyName("recorded_at")]
    public DateTime RecordedAt { get; set; }
    [JsonPropertyName("warehouse_id")]
    public int WarehouseId { get; set; }
    [JsonIgnore]
    public Warehouse Warehouse { get; set; }

    public StorageViolation()
    {
    }

    public StorageViolation(StorageViolationCreateModel model)
    {
        Temperature = model.Temperature;
        Humidity = model.Humidity;
        RecordedAt = model.RecordedAt;
        WarehouseId = model.WarehouseId;
    }
}
