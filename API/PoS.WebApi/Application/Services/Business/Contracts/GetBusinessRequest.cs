using System.Text.Json.Serialization;

namespace PoS.WebApi.Application.Services.Business.Contracts;

public class GetBusinessRequest
{
    [JsonIgnore]
    public Guid Id { get; set; }
}