using System.Text.Json.Serialization;

namespace MediStoS.DataTransferObjects;

public class MedicineCreateModel
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    [JsonPropertyName("manufacturer")]
    public string Manufacturer { get; set; } = string.Empty;
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    [JsonPropertyName("max_temperature")]
    public float MaxTemperature { get; set; }
    [JsonPropertyName("min_temperature")]
    public float MinTemperature { get; set; }
    [JsonPropertyName("max_humidity")]
    public float MaxHumidity { get; set; }
    [JsonPropertyName("min_humidity")]
    public float MinHumidity { get; set; }
}
