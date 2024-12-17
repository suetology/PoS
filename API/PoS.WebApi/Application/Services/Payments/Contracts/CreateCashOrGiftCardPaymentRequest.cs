using System.Text.Json.Serialization;
using PoS.WebApi.Domain.Enums;

namespace PoS.WebApi.Application.Services.Payments.Contracts;

public class CreateCashOrGiftCardPaymentRequest
{
    [JsonIgnore]
    public Guid BusinessId { get; set; }
    
    public Guid OrderId { get; set; }
    
    public decimal PaymentAmount { get; set; }
    
    public PaymentMethod PaymentMethod { get; set; }
}