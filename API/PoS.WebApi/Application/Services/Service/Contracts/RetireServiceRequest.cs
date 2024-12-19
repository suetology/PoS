using System.Text.Json.Serialization;

namespace PoS.WebApi.Application.Services.Service.Contracts;

public class RetireServiceRequest
{
    [JsonIgnore]
    public Guid Id { get; set; }

    [JsonIgnore]
    public Guid BusinessId { get; set; }
}