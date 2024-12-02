using System.Text.Json.Serialization;

namespace PoS.WebApi.Application.Services.ItemGroup.Contracts;

public class GetAllItemGroupsRequest
{
    [JsonIgnore]
    public Guid BusinessId { get; set; }
    
    [JsonIgnore]
    public QueryParameters QueryParameters { get; set; } 
}