namespace PoS.WebApi.Application.Services.Customer.Contracts;

public class GetAllCustomersResponse
{
    public IEnumerable<CustomerDto> Customers { get; set; }
}