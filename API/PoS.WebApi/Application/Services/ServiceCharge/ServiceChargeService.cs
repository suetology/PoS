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
            
        public async Task<GetAllServiceChargesResponse> GetServiceCharges(GetAllServiceChargesRequest request)
        {
            var serviceCharges = await _serviceChargeRepository.GetAll();
            var serviceChargesDtos = serviceCharges
                .Where(s => s.BusinessId == request.BusinessId)
                .Select(s => new ServiceChargeDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    Value = s.Value,
                    IsPercentage = s.IsPercentage
                });

            return new GetAllServiceChargesResponse
            {
                ServiceCharges = serviceChargesDtos
            };
        }

        public async Task CreateServiceCharge(CreateServiceChargeRequest request)
        {
            var serviceCharge = new Domain.Entities.ServiceCharge
            {
                BusinessId = request.BusinessId,
                Name = request.Name,
                Description = request.Description,
                Value = request.Value,
                IsPercentage = request.IsPercentage
            };
            
            await _serviceChargeRepository.Create(serviceCharge);
            await _unitOfWork.SaveChanges();
        }

        public async Task UpdateServiceCharge(UpdateServiceChargeRequest request)
        {
            var existingServiceCharge = await _serviceChargeRepository.Get(request.Id);

            if (existingServiceCharge == null || existingServiceCharge.BusinessId != request.BusinessId)
            {
                throw new KeyNotFoundException("Service charge not found.");
            }

            existingServiceCharge.Name = request.Name ?? existingServiceCharge.Name;
            existingServiceCharge.Description = request.Description ?? existingServiceCharge.Description;
            existingServiceCharge.Value = request.Value ?? existingServiceCharge.Value;
            existingServiceCharge.IsPercentage = request.IsPercentage ?? existingServiceCharge.IsPercentage;

            await _serviceChargeRepository.Update(existingServiceCharge);
            await _unitOfWork.SaveChanges();
        }

    }
}
