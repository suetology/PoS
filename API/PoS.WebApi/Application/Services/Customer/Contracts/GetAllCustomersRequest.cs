using System.Text.Json.Serialization;

namespace PoS.WebApi.Application.Services.Customer.Contracts;

public class GetAllCustomersRequest
{
    [JsonIgnore]
    public Guid BusinessId { get; set; }
}