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
                    IsPercentage = s.IsPercentage,
                    Retired = s.Retired
                });

            return new GetAllServiceChargesResponse
            {
                ServiceCharges = serviceChargesDtos
            };
        }

        public async Task<GetAllServiceChargesResponse> GetValidServiceCharges(GetAllServiceChargesRequest request)
        {
            var serviceCharges = await _serviceChargeRepository.GetAll();
            var serviceChargesDtos = serviceCharges
                .Where(s => s.BusinessId == request.BusinessId && false == s.Retired)
                .Select(s => new ServiceChargeDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    Value = s.Value,
                    IsPercentage = s.IsPercentage,
                    Retired = s.Retired
                });

            return new GetAllServiceChargesResponse
            {
                ServiceCharges = serviceChargesDtos
            };
        }

        public async Task<GetServiceChargeResponse> GetServiceCharge(GetServiceChargeRequest request)
        {
            var serviceCharge = await _serviceChargeRepository.Get(request.Id);

            if (serviceCharge == null || serviceCharge.BusinessId != request.BusinessId)
            {
                throw new KeyNotFoundException("Service Charge is not found");
            }

            return new GetServiceChargeResponse
            {
                ServiceCharge = new ServiceChargeDto
                {
                    Id = serviceCharge.Id,
                    Name = serviceCharge.Name,
                    Description = serviceCharge.Description,
                    Value = serviceCharge.Value,
                    IsPercentage = serviceCharge.IsPercentage,
                    Retired = serviceCharge.Retired,
                }
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
                IsPercentage = request.IsPercentage,
                Retired = false
            };
            
            await _serviceChargeRepository.Create(serviceCharge);
            await _unitOfWork.SaveChanges();
        }

        public async Task UpdateServiceCharge(UpdateServiceChargeRequest request)
        {
            var existingServiceCharge = await _serviceChargeRepository.Get(request.Id);

            if (existingServiceCharge == null || existingServiceCharge.BusinessId != request.BusinessId || true == existingServiceCharge.Retired)
            {
                throw new KeyNotFoundException("Service charge not found or unauthorised.");
            }

            var updatedServiceCharge = new Domain.Entities.ServiceCharge
            {
                BusinessId = existingServiceCharge.BusinessId,
                Name = request.Name,
                Description = request.Description,
                Value = request.Value ?? existingServiceCharge.Value,
                IsPercentage = request.IsPercentage ?? existingServiceCharge.IsPercentage,
                Retired = false
            };
            await _serviceChargeRepository.Create(updatedServiceCharge);

            existingServiceCharge.Retired = true;
            await _serviceChargeRepository.Update(existingServiceCharge);

            await _unitOfWork.SaveChanges();
        }

        public async Task RetireServiceCharge(RetireServiceChargeRequest request)
        {
            var existingServiceCharge = await _serviceChargeRepository.Get(request.Id);
            if (existingServiceCharge == null || existingServiceCharge.BusinessId != request.BusinessId || true == existingServiceCharge.Retired)
            {
                throw new KeyNotFoundException("Tax not found or unauthorised.");
            }

            existingServiceCharge.Retired = true;

            await _serviceChargeRepository.Update(existingServiceCharge);
            await _unitOfWork.SaveChanges();
        }
    }
}
