using Microsoft.EntityFrameworkCore.Storage;

namespace PoS.WebApi.Domain.Common;

public interface IUnitOfWork
{
    Task<int> SaveChanges();

    Task<IDbContextTransaction> BeginTransaction();
}