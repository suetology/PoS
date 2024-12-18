using Newtonsoft.Json;

namespace PoS.WebApi.Application.Services.Refund.Contracts;

public class CreateRefundRequest
{
    [JsonIgnore]
    public Guid BusinessId { get; set; }

    public Guid OrderId { get; set; }

    public string Reason { get; set; }
}