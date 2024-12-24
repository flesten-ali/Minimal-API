using Microsoft.IdentityModel.Tokens;
using MinimalAPI.Modles;
namespace MinimalAPI.Services;

public interface IJWTTokenServices
{
    Task<JWTToken?> GenerateToken(AuthenticationRequestBody authenticationRequestBody);
    bool ValidateToken(JWTToken token, out SecurityToken securityToken);
}
