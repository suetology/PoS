using System.Text.Json.Serialization;

namespace PoS.WebApi.Application.Services.Item.Contracts;

public class GetAllItemsRequest
{
    [JsonIgnore]
    public Guid BusinessId { get; set; }
    
    [JsonIgnore]
    public QueryParameters QueryParameters { get; set; }
}