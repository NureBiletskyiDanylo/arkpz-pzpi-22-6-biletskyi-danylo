using System.Text.Json.Serialization;

namespace MediStoS.DataTransferObjects;

public class BatchCreateModel
{
    [JsonPropertyName("batch_number")]
    public string BatchNumber { get; set; } = string.Empty;
    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }
    [JsonPropertyName("manufacture_date")]
    public DateTime ManufactureDate { get; set; }
    [JsonPropertyName("expiration_date")]
    public DateTime ExpirationDate { get; set; }
    [JsonPropertyName("warehouse_id")]
    public int WarehouseId { get; set; }
    [JsonPropertyName("user_id")]
    public int UserId { get; set; }
    [JsonPropertyName("medicine_id")]
    public int MedicineId { get; set; }

}
