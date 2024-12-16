using PoS.WebApi.Application.Repositories;
using PoS.WebApi.Application.Services.Payments.Contracts;
using PoS.WebApi.Domain.Common;
using PoS.WebApi.Domain.Enums;
using Stripe;
using Stripe.Checkout;

namespace PoS.WebApi.Application.Services.Payments;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public PaymentService(
        IPaymentRepository paymentRepository, 
        IOrderRepository orderRepository,
        IUnitOfWork unitOfWork)
    {
        _paymentRepository = paymentRepository;
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateCheckoutSessionResponse> CreateCheckoutSession(CreateCheckoutSessionRequest request)
    {
        var order = await _orderRepository.Get(request.OrderId);

        if (order == null || (order.Status != OrderStatus.Open && order.Status != OrderStatus.PartiallyPaid))
        {
            return null;
        }

        var options = new SessionCreateOptions
        {
            LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (int)(request.PaymentAmount * 100),
                        Currency = "eur",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = $"Order {request.OrderId}",
                        }
                    },
                    Quantity = 1
                }
            },
            Mode = "payment",
            SuccessUrl = "http://localhost:4242/success",
            CancelUrl = "http://localhost:4242/cancel",
        };

        var service = new SessionService();
        var session = service.Create(options);

        return new CreateCheckoutSessionResponse
        {
            SessionUrl = session.Url
        };
    }
}