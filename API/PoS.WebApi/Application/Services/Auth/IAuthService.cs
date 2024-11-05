using PoS.WebApi.Application.Services.Auth.Contracts;

namespace PoS.WebApi.Application.Services.Auth;

public interface IAuthService
{
    Task<LoginResponse> Login(LoginRequest request);

    Task<RefreshAccessTokenResponse> RefreshAccessToken(RefreshAccessTokenRequest request);
    
    Task Logout(LogoutRequest request);
}