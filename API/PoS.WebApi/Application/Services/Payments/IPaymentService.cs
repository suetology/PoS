using PoS.WebApi.Application.Services.Payments.Contracts;

namespace PoS.WebApi.Application.Services.Payments;

public interface IPaymentService
{
    Task<CreateCardPaymentResponse> CreateCardPayment(CreateCardPaymentRequest request);

    Task CreateCashOrGiftCardPayment(CreateCashOrGiftCardPaymentRequest request);
    
    Task HandleStripeWebhook(HandleStripeWebhookRequest request);
}