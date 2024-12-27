using System.Text.Json.Serialization;

namespace MediStoS.DataTransferObjects;

public class WarehouseCreateModel
{
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
}
