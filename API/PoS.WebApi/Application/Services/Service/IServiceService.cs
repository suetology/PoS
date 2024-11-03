namespace PoS.WebApi.Application.Services.Service;

using PoS.WebApi.Application.Services.Service.Contracts;
using Domain.Entities;


public interface IServiceService
    {
        Task<Service> GetService(Guid serviceId);
        Task<IEnumerable<ServiceDto>> GetServices(string sort, string order, int page, int pageSize);
        Task CreateService(ServiceDto serviceDto);
        Task UpdateService(Guid serviceId, ServiceDto serviceDto);
    }
