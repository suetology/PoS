using System.Text.Json.Serialization;

namespace PoS.WebApi.Application.Services.Order.Contracts;

public class GetOrderRequest
{
    [JsonIgnore]
    public Guid Id { get; set; }
    
    [JsonIgnore]
    public Guid BusinessId { get; set; }
}