
namespace PoS.WebApi.Application.Services.ServiceCharge.Contracts;

using Domain.Entities;
    public class ServiceChargeDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public bool IsPercentage { get; set; }

        public ServiceCharge ToDomain()
        {
            return new ServiceCharge
            {
                Name = Name,
                Description = Description,
                Value = Value,
                IsPercentage = IsPercentage,
                LastUpdated = DateTime.UtcNow
            };
        }
    }

