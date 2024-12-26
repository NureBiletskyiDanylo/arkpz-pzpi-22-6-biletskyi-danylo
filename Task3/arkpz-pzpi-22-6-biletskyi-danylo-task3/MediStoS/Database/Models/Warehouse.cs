using MediStoS.DataTransferObjects;
using System.Text.Json.Serialization;

namespace MediStoS.Database.Models;

public class Warehouse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    [JsonPropertyName("address")]
    public string Address { get; set; } = string.Empty;
    [JsonPropertyName("max_temperature")]
    public float MaxTemperature { get; set; }
    [JsonPropertyName("min_temperature")]
    public float MinTemperature { get; set; }
    [JsonPropertyName("max_humidity")]
    public float MaxHumidity { get; set; }
    [JsonPropertyName("min_humidity")]
    public float MinHumidity { get; set; }
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }
    [JsonIgnore]
    public ICollection<Batch> Batches = new List<Batch>();
    [JsonIgnore]
    public ICollection<StorageViolation> StorageViolations = new List<StorageViolation>();
    [JsonIgnore]
    public ICollection<Sensor> Sensors = new List<Sensor>();

    public Warehouse()
    {
    }
    public Warehouse(WarehouseCreateModel warehouse)
    {
        Name = warehouse.Name;
        Address = warehouse.Address;
        MaxTemperature = warehouse.MaxTemperature;
        MinTemperature = warehouse.MinTemperature;
        MaxHumidity = warehouse.MaxHumidity;
        MinHumidity = warehouse.MinHumidity;
        CreatedAt = warehouse.CreatedAt;
    }
}
