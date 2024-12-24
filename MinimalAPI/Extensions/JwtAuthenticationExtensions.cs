using Microsoft.IdentityModel.Tokens;
using MinimalAPI.Configuration;
using System.Text;
namespace MinimalAPI.Extensions;

public static class JwtAuthenticationExtensions
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtConfig = configuration.GetSection("JWTToken").Get<JWTConfiguration>();
        var key = Encoding.UTF8.GetBytes(jwtConfig.Key);

        services.AddAuthentication("Bearer")
                .AddJwtBearer(options =>
                { 
                         options.TokenValidationParameters = new()
                         {

                             ValidateIssuer = true,
                             ValidIssuer = jwtConfig.Issuer,
                             ValidateAudience = true,
                             ValidAudience = jwtConfig.Audience,
                             ValidateIssuerSigningKey = true,
                             IssuerSigningKey = new SymmetricSecurityKey(key),
                             ValidateLifetime = true,
                         };
                });
        return services;
    }
}