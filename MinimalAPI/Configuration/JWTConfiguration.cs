using System.ComponentModel.DataAnnotations;
namespace MinimalAPI.Configuration;

public class JWTConfiguration
{
    [Required]
    public string Key { get; set; }

    [Required]
    public string Issuer { get; set; }

    [Required]
    public string Audience { get; set; }

    [Required]
    public int ExpirationTimeInMinutes { get; set; }
}