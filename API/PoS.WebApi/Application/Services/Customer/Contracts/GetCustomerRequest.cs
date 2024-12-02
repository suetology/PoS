using System.Text.Json.Serialization;

namespace PoS.WebApi.Application.Services.Customer.Contracts;

public class GetCustomerRequest
{
    [JsonIgnore]
    public Guid Id { get; set; }
    
    [JsonIgnore]
    public Guid BusinessId { get; set; }
}