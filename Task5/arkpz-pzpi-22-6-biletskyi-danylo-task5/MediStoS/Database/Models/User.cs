using MediStoS.DataTransferObjects;
using MediStoS.Enums;
using System.Text.Json.Serialization;

namespace MediStoS.Database.Models;

public class User
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
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
    [JsonIgnore]
    public ICollection<Batch> Batches { get; set; } = new List<Batch>();

    public User()
    {
    }

    public User(UserCreateModel model)
    {
        FirstName = model.FirstName;
        LastName = model.LastName;
        Email = model.Email;
        Password = model.Password;
        Role = model.Role;
    }
}
