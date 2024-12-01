using Microsoft.EntityFrameworkCore;
using PoS.WebApi.Application.Repositories;
using PoS.WebApi.Domain.Entities;
using PoS.WebApi.Infrastructure.Persistence;

namespace PoS.WebApi.Infrastructure.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly DatabaseContext _dbContext;

    public RefreshTokenRepository(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<RefreshToken> Get(Guid id)
    {
        return await _dbContext.RefreshTokens.FindAsync(id);
    }

    public async Task Create(RefreshToken token)
    {
        await _dbContext.RefreshTokens.AddAsync(token);
    }

    public async Task<IEnumerable<RefreshToken>> GetAll()
    {
        return await _dbContext.RefreshTokens.ToListAsync();
    }

    public async Task Update(RefreshToken token)
    {
        _dbContext.RefreshTokens.Update(token);
        
        await Task.CompletedTask;
    }

    public async Task<RefreshToken> GetByAccessToken(string accessToken)
    {
        return await _dbContext.RefreshTokens.FirstOrDefaultAsync(t => t.AccessToken == accessToken);
    }

    public async Task<int> DeleteExpiredRefreshTokens()
    {
        return await _dbContext.RefreshTokens
            .Where(t => t.Expires < DateTime.Now)
            .ExecuteDeleteAsync();
    }

    public async Task Delete(RefreshToken refreshToken)
    {
        _dbContext.RefreshTokens.Remove(refreshToken);

        await Task.CompletedTask;
    }
}