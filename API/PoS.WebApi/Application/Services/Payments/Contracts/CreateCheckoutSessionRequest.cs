namespace PoS.WebApi.Application.Services.Payments.Contracts;

public class CreateCheckoutSessionRequest
{
    public Guid OrderId { get; set; }

    public decimal PaymentAmount { get; set; }
}