using System.Text.Json.Serialization;

namespace PoS.WebApi.Application.Services.Payments.Contracts;

public class RefundPaymentRequest
{
    [JsonIgnore]
    public Guid PaymentId { get; set; }
}