using System.Text.Json.Serialization;

namespace PoS.WebApi.Application.Services.ServiceCharge.Contracts;

public class RetireServiceChargeRequest
{
    [JsonIgnore]
    public Guid Id { get; set; }

    [JsonIgnore]
    public Guid BusinessId { get; set; }
}