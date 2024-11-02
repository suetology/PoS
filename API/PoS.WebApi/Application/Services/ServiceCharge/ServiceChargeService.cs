using PoS.WebApi.Application.Repositories;
using PoS.WebApi.Application.Services.ServiceCharge.Contracts;
using PoS.WebApi.Domain.Common;
using PoS.WebApi.Domain.Entities;

namespace PoS.WebApi.Application.Services.ServiceCharge
{
    public class ServiceChargeService : IServiceChargeService
    {
        private readonly IServiceChargeRepository _serviceChargeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ServiceChargeService(IServiceChargeRepository serviceChargeRepository, IUnitOfWork unitOfWork)
        {
            _serviceChargeRepository = serviceChargeRepository;
            _unitOfWork = unitOfWork;
        }
            
        public async Task<IEnumerable<ServiceChargeDto>> GetServiceCharges()
        {
            var serviceCharges = await _serviceChargeRepository.GetAll();
            return serviceCharges.Select(s => new ServiceChargeDto
            {
                Name = s.Name,
                Description = s.Description,
                Value = s.Value,
                IsPercentage = s.IsPercentage
            });
        }

        public async Task CreateServiceCharge(ServiceChargeDto serviceChargeDto)
        {
            var serviceCharge = serviceChargeDto.ToDomain();
            await _serviceChargeRepository.Create(serviceCharge);
            await _unitOfWork.SaveChanges();
        }

    }
}
