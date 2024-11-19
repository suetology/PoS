using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using PoS.WebApi.Application.Repositories;
using PoS.WebApi.Application.Services.Auth.Contracts;
using PoS.WebApi.Infrastructure.Security;

namespace PoS.WebApi.Application.Services.Auth;

public class AuthService : IAuthService
{
    private const string RoleClaimName = "role"; 
    
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
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Name, user.Name),
            new Claim(RoleClaimName, user.Role.ToString())
        };
        
        return new LoginResponse
        {
            AccessToken = await _jwtProvider.GenerateAccessToken(claims)
        };
    }

    public Task<RefreshAccessTokenResponse> RefreshAccessToken(RefreshAccessTokenRequest request)
    {
        throw new NotImplementedException();
    }

    public Task Logout(LogoutRequest request)
    {
        throw new NotImplementedException();
    }
}