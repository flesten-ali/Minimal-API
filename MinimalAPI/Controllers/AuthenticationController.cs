using Microsoft.AspNetCore.Mvc;
using MinimalAPI.Modles;
using MinimalAPI.Services;
namespace MinimalAPI.Controllers;

[Route("api/Authentication")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private IJWTTokenServices _jwtTokenServices;

    public AuthenticationController(IJWTTokenServices jwtTokenServices)
    {
        _jwtTokenServices = jwtTokenServices;
    }

    [HttpPost("authenticate")]
    public async Task<ActionResult<JWTToken>> Authenticate(AuthenticationRequestBody authenticationRequestBody)
    {
        if (authenticationRequestBody == null)
        {
            return BadRequest("Request body is missing.");
        }

        var jwtToken = await _jwtTokenServices.GenerateToken(authenticationRequestBody);

        if (jwtToken == null)
        {
            return Unauthorized("Invalid credentials.");
        }

        return Ok(jwtToken);
    }
}