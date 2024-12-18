using Microsoft.EntityFrameworkCore;
using PoS.WebApi.Application.Repositories;
using PoS.WebApi.Domain.Entities;
using PoS.WebApi.Infrastructure.Persistence;

namespace PoS.WebApi.Infrastructure.Repositories;

public class RefundRepository : IRefundRepository
{
    private readonly DatabaseContext _dbContext;

    public RefundRepository(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Create(Refund refund)
    {
        await _dbContext.Refunds.AddAsync(refund);
    }

    public async Task<Refund> Get(Guid id)
    {
        return await _dbContext.Refunds.FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IEnumerable<Refund>> GetAll()
    {
        return await _dbContext.Refunds.ToListAsync();
    }

    public async Task Update(Refund refund)
    {
        _dbContext.Refunds.Update(refund);
    }
}