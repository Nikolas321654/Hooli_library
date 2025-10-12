using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HomeLib.Core.Model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HomeLib.Infrastructure;

public class JwtService(IOptions<AuthSettings> authSettings)
{
    public string GenerateJwtToken(User userAccount)
    {
        var claims = new List<Claim>
        {
            new Claim("userName", userAccount.Name),
            new Claim("id", userAccount.Id.ToString()),
            new Claim("userLogin", userAccount.Login)
        };

        var jwtToken = new JwtSecurityToken(
            expires: DateTime.UtcNow.Add(authSettings.Value.TokenLifetime),
            claims: claims,
            signingCredentials:
            new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.Value.SecretKey)),
                SecurityAlgorithms.HmacSha256));
        
        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }
}