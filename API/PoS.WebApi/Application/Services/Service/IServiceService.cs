namespace PoS.WebApi.Application.Services.Service;

using PoS.WebApi.Application.Services.Service.Contracts;


public interface IServiceService
    {
        Task<GetServiceResponse> GetService(GetServiceRequest request);
        Task<GetAllServicesResponse> GetServices(GetAllServicesRequest request);
        Task<GetAllServicesResponse> GetActiveServices(GetAllServicesRequest request);
        Task CreateService(CreateServiceRequest request);
        Task UpdateService(UpdateServiceRequest request);
        Task<GetAvailableTimesResponse> GetAvailableTimes(GetAvailableTimesRequest request);
        Task RetireService(RetireServiceRequest request);
    }
