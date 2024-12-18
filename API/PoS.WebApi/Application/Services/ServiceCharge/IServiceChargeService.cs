using System.Collections.Generic;
using System.Threading.Tasks;
using PoS.WebApi.Application.Services.Service.Contracts;
using PoS.WebApi.Application.Services.ServiceCharge.Contracts;

namespace PoS.WebApi.Application.Services.ServiceCharge
{
    public interface IServiceChargeService
    {
        Task<GetAllServiceChargesResponse> GetServiceCharges(GetAllServiceChargesRequest request);
        Task<GetAllServiceChargesResponse> GetValidServiceCharges(GetAllServiceChargesRequest request);
        Task CreateServiceCharge(CreateServiceChargeRequest request);
        Task UpdateServiceCharge(UpdateServiceChargeRequest request);
    }
}
