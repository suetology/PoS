using PoS.WebApi.Domain.Common;
using PoS.WebApi.Domain.Entities;

namespace PoS.WebApi.Application.Repositories;

public interface IRefreshTokenRepository : IRepository<RefreshToken>
{
    Task<RefreshToken> GetByAccessToken(string accessToken);
    
    Task<int> DeleteExpiredRefreshTokens();

    Task Delete(RefreshToken refreshToken);
}