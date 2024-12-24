using Microsoft.OpenApi.Models;
namespace MinimalAPI.Extensions;

public static class CustomSwaggerExtensions
{
    public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(setupAction =>
        {
            setupAction.AddSecurityDefinition("API Authentication", new()
            {
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                Description = "Input a valid Token to be authenticated"
            });
            setupAction.AddSecurityRequirement(
                 new()
                 { 
                     { 
                         new()
                         {
                               Reference = new OpenApiReference()
                               {
                                Type = ReferenceType.SecurityScheme,
                                Id = "API Authentication"
                               }
                         }, new List<string>()
                     }
                 }
            );
        });
        return services;
    }
}
