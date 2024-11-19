using PoS.WebApi.Domain.Common;
using PoS.WebApi.Domain.Entities;

namespace PoS.WebApi.Application.Repositories;

public interface ITaxRepository : IRepository<Tax>
{
    Task<IEnumerable<Tax>> GetTaxesByIds(List<Guid> taxIds);
}