using System.Security.Claims;
using PoS.WebApi.Domain.Entities;

namespace PoS.WebApi.Infrastructure.Security;

public interface IJwtProvider
{
    Task<string> GenerateAccessToken(IEnumerable<Claim> claims);

    Task<string> GenerateRefreshToken(string accessToken);

    Task<(string, string)> RefreshAccessToken(string refreshToken);
    
    Task RevokeTokens(string refreshToken);
}