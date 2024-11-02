using System.Collections.Generic;
using System.Threading.Tasks;
using PoS.WebApi.Domain.Common;
using PoS.WebApi.Domain.Entities;

namespace PoS.WebApi.Application.Repositories;

public interface IServiceChargeRepository
{
    Task<IEnumerable<ServiceCharge>> GetAllServiceCharges();
    Task<ServiceCharge> Get(Guid id);
    Task Create(ServiceCharge serviceCharge);
}




