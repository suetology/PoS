using System.Text.Json.Serialization;

namespace PoS.WebApi.Application.Services.Order.Contracts;

public class RetireOpenOrdersRequest
{
    [JsonIgnore]
    public Guid BusinessId { get; set; }

    public Guid CustomerId { get; set; }
}