using PoS.WebApi.Domain.Common;
using PoS.WebApi.Domain.Entities;

namespace PoS.WebApi.Application.Repositories;

public interface IBusinessRepository : IRepository<Business>
{
    Task<IEnumerable<Business>> GetAll();
    void Update(Business business);
}
