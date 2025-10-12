using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace HomeLib.Infrastructure;

public static class AuthExtensions
{
    public static IServiceCollection AddAuth(this IServiceCollection servicesCollection, IConfiguration configuration)
    {
        var authSettings = configuration.GetSection(nameof(AuthSettings))
            .Get<AuthSettings>();

        if (string.IsNullOrEmpty(authSettings?.SecretKey))
        {
            throw new InvalidOperationException(
                "JWT Secret Key is missing"
            );
        }

        var securityKey = Encoding.UTF8.GetBytes(authSettings.SecretKey);

        servicesCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(securityKey)
                };
            });
        return servicesCollection;
    }
}