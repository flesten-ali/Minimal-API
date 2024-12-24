using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MinimalAPI.Configuration;
using MinimalAPI.Interfaces;
using MinimalAPI.Modles;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace MinimalAPI.Services;

public class JWTTokenGenerator : IJWTTokenServices
{
    private JWTConfiguration _jwtConfiguration;
    private IUser _user;

    public JWTTokenGenerator(IOptions<JWTConfiguration> options, IUser user)
    {
        _jwtConfiguration = options.Value;
        _user = user;
    }

    public async Task<JWTToken?> GenerateToken(AuthenticationRequestBody requestBody)
    {
        var user = await _user.GetUser(requestBody.UserName, requestBody.Password);
        if (user is null)
        {
            return null;
        }

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.Key));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var claims = new List<Claim>
        {
                new ("sub", user.Id.ToString()),
                new ("given_name", user.FirstName),
                new ("family_name", user.LastName),
        };

        var securityToken = new JwtSecurityToken(
            _jwtConfiguration.Issuer,
            _jwtConfiguration.Audience,
            claims,
            DateTime.Now,
            DateTime.Now.AddMinutes(_jwtConfiguration.ExpirationTimeInMinutes),
            signingCredentials);

        var tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
        return new JWTToken { Token = tokenString };
    }

    public bool ValidateToken(JWTToken jwtToken, out SecurityToken securityToken)
    {
        securityToken = null;
        var validationParameters = GetValidationParameters();
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.ValidateToken(jwtToken.Token, validationParameters, out _);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private TokenValidationParameters GetValidationParameters()
    {
        return new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidIssuer = _jwtConfiguration.Issuer,
            ValidateAudience = true,
            ValidAudience = _jwtConfiguration.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.Key)),
            ValidateLifetime = true,
        };
    }
}