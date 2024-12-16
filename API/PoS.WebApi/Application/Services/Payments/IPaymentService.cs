using PoS.WebApi.Application.Services.Payments.Contracts;

namespace PoS.WebApi.Application.Services.Payments;

public interface IPaymentService
{
    Task<CreateCheckoutSessionResponse> CreateCheckoutSession(CreateCheckoutSessionRequest request);
}