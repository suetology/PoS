using System.Data;
using Microsoft.EntityFrameworkCore.Storage;
using PoS.WebApi.Domain.Common;

namespace PoS.WebApi.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly DatabaseContext _dbContext;
    
    public UnitOfWork(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> SaveChanges()
    {
        return await _dbContext.SaveChangesAsync();
    }

    public async Task<IDbContextTransaction> BeginTransaction()
    {
        return await _dbContext.Database.BeginTransactionAsync();
    }
}