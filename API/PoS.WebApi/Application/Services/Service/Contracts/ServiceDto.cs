﻿namespace PoS.WebApi.Application.Services.Service.Contracts
{
    public class ServiceDto
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Duration { get; set; }

        public bool IsActive { get; set; } = true;

        public Guid EmployeeId { get; set; }
    }
}
