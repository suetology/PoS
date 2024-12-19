using System.Text.Json.Serialization;

namespace PoS.WebApi.Application.Services.User.Contracts;

public class RetireUserRequest
{
    [JsonIgnore]
    public Guid Id { get; set; }

    [JsonIgnore]
    public Guid BusinessId { get; set; }
}