namespace PoS.WebApi.Application.Services.Service;

using PoS.WebApi.Application.Services.Service.Contracts;
using Domain.Entities;


public interface IServiceService
    {
        Task<GetServiceResponse> GetService(GetServiceRequest request);
        Task<GetAllServicesResponse> GetServices(GetAllServicesRequest request);
        Task CreateService(CreateServiceRequest request);
        Task UpdateService(UpdateServiceRequest request);
        Task<GetAvailableTimesResponse> GetAvailableTimes(GetAvailableTimesRequest request);
    }
