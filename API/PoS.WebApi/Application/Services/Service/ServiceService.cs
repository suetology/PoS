namespace PoS.WebApi.Application.Services.Service;

using PoS.WebApi.Application.Repositories;
using PoS.WebApi.Application.Services.Service.Contracts;
using PoS.WebApi.Domain.Entities;
using PoS.WebApi.Domain.Common;


    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ServiceService(IServiceRepository serviceRepository, IUnitOfWork unitOfWork)
        {
            _serviceRepository = serviceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<GetServiceResponse> GetService(Guid serviceId)
        { 
            var service = await _serviceRepository.Get(serviceId);

            return new GetServiceResponse
            {
                Service = new ServiceDto
                {
                    Name = service.Name,
                    Description = service.Description,
                    Price = service.Price,
                    Duration = service.Duration,
                    IsActive = service.IsActive,
                    EmployeeId = service.EmployeeId
                }
            };
        }

        public async Task<GetServicesResponse> GetServices(string sort, string order, int page, int pageSize)
        {
            var services = await _serviceRepository.GetServices(sort, order, page, pageSize);
            var serviceDtos = services
                .Select(s => new ServiceDto
                {
                    Name = s.Name,
                    Description = s.Description,
                    Price = s.Price,
                    Duration = s.Duration,
                    IsActive = s.IsActive,
                    EmployeeId = s.EmployeeId
                });

            return new GetServicesResponse
            {
                Services = serviceDtos
            };
        }

        public async Task CreateService(CreateServiceRequest request)
        {
            var service = new Service
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Duration = request.Duration,
                IsActive = request.IsActive,
                EmployeeId = request.EmployeeId
            };
            
            await _serviceRepository.Create(service);
            await _unitOfWork.SaveChanges();
        }

        public async Task UpdateService(Guid serviceId, UpdateServiceRequest request)
        {
            var existingService = await _serviceRepository.Get(serviceId);

            if (existingService == null)
            {
                throw new KeyNotFoundException("Service not found.");
            }
            
            existingService.Name = request.Name ?? existingService.Name;
            existingService.Description = request.Description ?? existingService.Description;
            existingService.Price = request.Price ?? existingService.Price;
            existingService.Duration = request.Duration ?? existingService.Duration;
            existingService.IsActive = request.IsActive ?? existingService.IsActive;
            existingService.EmployeeId = request.EmployeeId ?? existingService.EmployeeId;

            await _serviceRepository.Update(existingService);
            await _unitOfWork.SaveChanges();
        }
    }