using MediStoS.DataTransferObjects;
using System.Text.Json.Serialization;

namespace MediStoS.Database.Models;

public class Medicine
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
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
    [JsonIgnore]
    public ICollection<Batch> Batches = new List<Batch>();

    public Medicine()
    {
    }

    public Medicine(MedicineCreateModel model)
    {
        Name = model.Name;
        Manufacturer = model.Manufacturer;
        Description = model.Description;
        MaxTemperature = model.MaxTemperature;
        MinTemperature = model.MinTemperature;
        MaxHumidity = model.MaxHumidity;
        MinHumidity = model.MinHumidity;
    }
}
