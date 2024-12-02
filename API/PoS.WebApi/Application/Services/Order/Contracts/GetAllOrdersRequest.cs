using System.Text.Json.Serialization;

namespace PoS.WebApi.Application.Services.Order.Contracts;

public class GetAllOrdersRequest
{
    [JsonIgnore]
    public Guid BusinessId { get; set; }

    [JsonIgnore]
    public OrderQueryParameters QueryParameters { get; set; }
}