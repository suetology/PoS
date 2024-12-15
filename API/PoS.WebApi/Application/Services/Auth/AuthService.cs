using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using PoS.WebApi.Application.Repositories;
using PoS.WebApi.Application.Services.Auth.Contracts;
using PoS.WebApi.Infrastructure.Security;
using PoS.WebApi.Infrastructure.Security.Extensions;

namespace PoS.WebApi.Application.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IUserRepository _userRepository;

    public AuthService(
        IJwtProvider jwtProvider,
        IUserRepository userRepository)
    {
        _jwtProvider = jwtProvider;
        _userRepository = userRepository;
    }
    
    public async Task<LoginResponse> Login(LoginRequest request)
    {
        var user = await _userRepository.GetByUsername(request.Username);
        
        if (user == null || user.PasswordHash != request.Password) //passwordo checka reiks pakeist veliau
        {
            throw new InvalidCredentialException("Invalid credentials.");
        }

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Name, user.Name),
            new Claim(ClaimsPrincipalExtensions.UserIdClaimName, user.Id.ToString()),
            new Claim(ClaimsPrincipalExtensions.RoleClaimName, user.Role.ToString()),
            new Claim(ClaimsPrincipalExtensions.BusinessIdClaimName, user.BusinessId.ToString())
        };

        var accessToken = await _jwtProvider.GenerateAccessToken(claims);
        var refreshToken = await _jwtProvider.GenerateRefreshToken(accessToken);
        
        return new LoginResponse
        {
            UserId = user.Id,
            BusinessId = user.BusinessId.Value,
            Role = user.Role,
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    public async Task<RefreshAccessTokenResponse> RefreshAccessToken(RefreshAccessTokenRequest request)
    {
        var tokens = await _jwtProvider.RefreshAccessToken(request.RefreshToken);

        return new RefreshAccessTokenResponse
        {
            RefreshToken = tokens.Item1,
            AccessToken = tokens.Item2
        };
    }

    public Task Logout(LogoutRequest request)
    {
        return _jwtProvider.RevokeTokens(request.RefreshToken);
    }
}