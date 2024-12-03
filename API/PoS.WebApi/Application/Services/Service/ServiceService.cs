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

        public async Task<GetServiceResponse> GetService(GetServiceRequest request)
        { 
            var service = await _serviceRepository.Get(request.Id);

            return new GetServiceResponse
            {
                Service = new ServiceDto
                {
                    Id = service.Id,
                    Name = service.Name,
                    Description = service.Description,
                    Price = service.Price,
                    Duration = service.Duration,
                    IsActive = service.IsActive,
                    EmployeeId = service.EmployeeId
                }
            };
        }

        public async Task<GetAllServicesResponse> GetServices(GetAllServicesRequest request)
        {
            var services = await _serviceRepository.GetServices(request.Sort, request.Order, request.Page, request.PageSize);
            var serviceDtos = services
                .Where(s => s.BusinessId == request.BusinessId)
                .Select(s => new ServiceDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    Price = s.Price,
                    Duration = s.Duration,
                    IsActive = s.IsActive,
                    EmployeeId = s.EmployeeId
                });

            return new GetAllServicesResponse
            {
                Services = serviceDtos
            };
        }

        public async Task CreateService(CreateServiceRequest request)
        {
            var service = new Service
            {
                BusinessId = request.BusinessId,
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

        public async Task UpdateService(UpdateServiceRequest request)
        {
            var existingService = await _serviceRepository.Get(request.Id);

            if (existingService == null || existingService.BusinessId != request.BusinessId)
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