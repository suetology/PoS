using Microsoft.AspNetCore.Mvc;
using PoS.WebApi.Application.Services.Auth;
using PoS.WebApi.Application.Services.Auth.Contracts;

namespace PoS.WebApi.Presentation.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("login", Name = nameof(Login))]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var response = await _authService.Login(request);

        return Ok(response);
    }

   

    [HttpPost("refresh-token", Name = nameof(RefreshAccessToken))]
    [ProducesResponseType(typeof(RefreshAccessTokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> RefreshAccessToken(RefreshAccessTokenRequest request)
    {
        var response = await _authService.RefreshAccessToken(request);
        
        return Ok(response);
    }
    
    [HttpPost("logout", Name = nameof(Logout))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Logout(LogoutRequest request)
    {
        await _authService.Logout(request);

        return NoContent();
    }
}