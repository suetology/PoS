using System.Text.Json.Serialization;

namespace PoS.WebApi.Application.Services.Item.Contracts;

public class GetItemRequest
{
    [JsonIgnore]
    public Guid BusinessId { get; set; }
    
    [JsonIgnore]
    public Guid ItemId { get; set; }
}