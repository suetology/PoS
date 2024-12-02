using System.Text.Json.Serialization;

namespace PoS.WebApi.Application.Services.Customer.Contracts;

public class CreateCustomerRequest
{
    [JsonIgnore]
    public Guid BusinessId { get; set; }
    
    public string Name { get; set; }
    
    public string Email { get; set; }
    
    public string PhoneNumber { get; set; }
}