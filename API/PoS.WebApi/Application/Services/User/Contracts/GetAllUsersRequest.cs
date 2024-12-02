using System.Text.Json.Serialization;

namespace PoS.WebApi.Application.Services.User.Contracts;

public class GetAllUsersRequest
{
    [JsonIgnore]
    public Guid BusinessId { get; set; }
    
    [JsonIgnore]
    public QueryParameters QueryParameters { get; set; }
}