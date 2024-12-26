using MediStoS.Enums;
using System.Text.Json.Serialization;

namespace MediStoS.DataTransferObjects;

public class UserCreateModel
{
    [JsonPropertyName("first_name")]
    public string FirstName { get; set; } = string.Empty;
    [JsonPropertyName("last_name")]
    public string LastName { get; set; } = string.Empty;
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;
    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;
    [JsonPropertyName("role")]
    public Roles Role { get; set; }
}
