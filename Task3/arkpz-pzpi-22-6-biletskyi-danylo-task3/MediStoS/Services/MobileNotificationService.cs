using Newtonsoft.Json;
using System.Text;

namespace MediStoS.Services;
// -------------For now doesn't work. Needs an Android App---------------
public class MobileNotificationService
{
    private const string FcmUrl = "https://fcm.googleapis.com/fcm/send";
    private readonly string _serverKey = ""; 

    public async Task SendNotificationAsync(string deviceToken, string title, string body)
    {
        using var client = new HttpClient();

        var payload = new
        {
            to = deviceToken,
            notification = new
            {
                title = title,
                body = body
            }
        };

        var jsonPayload = JsonConvert.SerializeObject(payload);

        var request = new HttpRequestMessage(HttpMethod.Post, FcmUrl)
        {
            Headers = { { "Authorization", $"key={_serverKey}" } },
            Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json")
        };

        var response = await client.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error");
        }
    }
}
