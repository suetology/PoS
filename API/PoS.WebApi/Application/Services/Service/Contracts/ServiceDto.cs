namespace PoS.WebApi.Application.Services.Service.Contracts
{
    using PoS.WebApi.Domain.Entities;

    public class ServiceDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Duration { get; set; }

        public bool IsActive { get; set; }

        public Guid EmployeeId { get; set; }

        public Service ToDomain()
        {
            return new Service
            {
                Name = Name,
                Description = Description,
                Price = Price,
                Duration = Duration,
                IsActive = IsActive,
                EmployeeId = EmployeeId
            };
        }
    }
}
