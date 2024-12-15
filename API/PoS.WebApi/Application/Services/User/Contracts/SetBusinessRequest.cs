using System.Text.Json.Serialization;

namespace PoS.WebApi.Application.Services.User.Contracts;

public class SetBusinessRequest
{
    [JsonIgnore]
    public Guid UserId { get; set; }

    public Guid BusinessId { get; set; }
}