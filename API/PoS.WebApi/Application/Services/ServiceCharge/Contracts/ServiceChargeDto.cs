
namespace PoS.WebApi.Application.Services.ServiceCharge.Contracts;

using Domain.Entities;
    public class ServiceChargeDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public bool IsPercentage { get; set; }
    }

