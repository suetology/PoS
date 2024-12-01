namespace PoS.WebApi.Application.Services.Customer.Contracts;

public class CreateCustomerRequest
{
    public string Name { get; set; }
    
    public string Email { get; set; }
    
    public string PhoneNumber { get; set; }
}