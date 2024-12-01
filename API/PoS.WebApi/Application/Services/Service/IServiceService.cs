namespace PoS.WebApi.Application.Services.Service;

using PoS.WebApi.Application.Services.Service.Contracts;
using Domain.Entities;


public interface IServiceService
    {
        Task<GetServiceResponse> GetService(Guid serviceId);
        Task<GetServicesResponse> GetServices(string sort, string order, int page, int pageSize);
        Task CreateService(CreateServiceRequest request);
        Task UpdateService(Guid serviceId, UpdateServiceRequest request);
    }
