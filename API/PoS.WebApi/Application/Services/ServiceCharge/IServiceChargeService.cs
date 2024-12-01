using System.Collections.Generic;
using System.Threading.Tasks;
using PoS.WebApi.Application.Services.ServiceCharge.Contracts;

namespace PoS.WebApi.Application.Services.ServiceCharge
{
    public interface IServiceChargeService
    {
        Task<GetAllServiceChargesResponse> GetServiceCharges();
        Task CreateServiceCharge(CreateServiceChargeRequest request);
    }
}
