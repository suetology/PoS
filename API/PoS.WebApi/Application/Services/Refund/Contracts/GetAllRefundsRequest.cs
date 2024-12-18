using System.Text.Json.Serialization;

namespace PoS.WebApi.Application.Services.Refund.Contracts;

public class GetAllRefundsRequest
{
    [JsonIgnore]
    public Guid BusinessId { get; set; }
}