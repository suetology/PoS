namespace PoS.WebApi.Application.Services.Service.Contracts;

public class CreateServiceRequest
{
    public string Name { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public int Duration { get; set; }

    public bool IsActive { get; set; }

    public Guid EmployeeId { get; set; }
}