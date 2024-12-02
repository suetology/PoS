using System.Text.Json.Serialization;

namespace PoS.WebApi.Application.Services.ServiceCharge.Contracts;

public class GetAllServiceChargesRequest
{
    [JsonIgnore]
    public Guid BusinessId { get; set; }
}