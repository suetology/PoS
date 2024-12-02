using System.Text.Json.Serialization;

namespace PoS.WebApi.Application.Services.Discount.Contracts;

public class GetAllDiscountsRequest
{
    [JsonIgnore]
    public Guid BusinessId { get; set; }
    
    [JsonIgnore]
    public QueryParameters QueryParameters { get; set; }
}