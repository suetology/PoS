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

        public async Task<Service> GetService(Guid serviceId)
        {
            return await _serviceRepository.Get(serviceId);
        }

        public async Task<IEnumerable<ServiceDto>> GetServices(string sort, string order, int page, int pageSize)
        {
            var services = await _serviceRepository.GetServices(sort, order, page, pageSize);

            // Map each Service entity to a ServiceDto
            return services.Select(service => new ServiceDto
            {
                Name = service.Name,
                Description = service.Description,
                Price = service.Price,
                Duration = service.Duration,
                IsActive = service.IsActive,
                EmployeeId = service.EmployeeId
            });
        }

        public async Task CreateService(ServiceDto serviceDto)
        {
            var service = serviceDto.ToDomain();
            await _serviceRepository.Create(service);
            await _unitOfWork.SaveChanges();
        }

        public async Task UpdateService(Guid serviceId, ServiceDto serviceDto)
        {
            var existingService = await _serviceRepository.Get(serviceId);

            if (existingService != null)
            {
                existingService.Name = serviceDto.Name;
                existingService.Description = serviceDto.Description;
                existingService.Price = serviceDto.Price;
                existingService.Duration = serviceDto.Duration;
                existingService.IsActive = serviceDto.IsActive;
                existingService.EmployeeId = serviceDto.EmployeeId;

                _serviceRepository.Update(existingService);
                await _unitOfWork.SaveChanges();
            }
            else
            {
                throw new KeyNotFoundException("Service not found.");
            }
        }
    }