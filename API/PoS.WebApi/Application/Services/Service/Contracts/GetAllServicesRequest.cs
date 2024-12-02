using System.Text.Json.Serialization;

namespace PoS.WebApi.Application.Services.Service.Contracts;

public class GetAllServicesRequest
{
    [JsonIgnore]
    public Guid BusinessId { get; set; }
    
    public string Sort { get; set; }
    
    public string Order { get; set; }
    
    public int Page { get; set; }
    
    public int PageSize { get; set; }
}