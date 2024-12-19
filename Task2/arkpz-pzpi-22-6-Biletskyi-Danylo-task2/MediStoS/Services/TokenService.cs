using MediStoS.Database.Models;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace MediStoS.Services;

public class TokenService(IConfiguration configuration)
{
    public string Create(User user)
    {
        string secretKey = configuration["Jwt:Secret"]!;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("Role", user.Role.ToString())
                ]),
            Expires = DateTime.UtcNow.AddHours(6),
            SigningCredentials = credentials,
        };

        var handler = new JsonWebTokenHandler();
        var token = handler.CreateToken(tokenDescriptor);
        return token;
    }
}
