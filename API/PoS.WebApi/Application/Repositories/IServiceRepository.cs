using PoS.WebApi.Domain.Common;
using PoS.WebApi.Domain.Entities;

namespace PoS.WebApi.Application.Repositories
{
    public interface IServiceRepository : IRepository <Service>
    {
        Task<IEnumerable<Service>> GetServices(string sort, string order, int page, int pageSize);
        Task Update(Service service);
    }
}
