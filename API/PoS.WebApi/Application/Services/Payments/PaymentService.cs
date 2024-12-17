using Newtonsoft.Json;
using PoS.WebApi.Application.Repositories;
using PoS.WebApi.Application.Services.Payments.Contracts;
using PoS.WebApi.Domain.Common;
using PoS.WebApi.Domain.Entities;
using PoS.WebApi.Domain.Enums;
using Stripe;
using Stripe.Checkout;
using PaymentMethod = PoS.WebApi.Domain.Enums.PaymentMethod;

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

    public async Task<CreateCardPaymentResponse> CreateCardPayment(CreateCardPaymentRequest request)
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
            SuccessUrl = $"http://localhost:4200/order/{request.OrderId}",
            CancelUrl = $"http://localhost:4200/order/{request.OrderId}",
        };

        var service = new SessionService();
        var session = await service.CreateAsync(options);
        
        var payment = new Payment
        {
            BusinessId = request.BusinessId,
            Method = PaymentMethod.CreditOrDebitCard,
            State = PaymentState.InProgress,
            Amount = request.PaymentAmount,
            StripeCheckoutSessionId = session.Id,
            Date = DateTime.UtcNow,
            OrderId = request.OrderId
        };

        await _paymentRepository.Create(payment);
        await _unitOfWork.SaveChanges();
        
        return new CreateCardPaymentResponse
        {
            SessionUrl = session.Url
        };
    }

    public async Task CreateCashOrGiftCardPayment(CreateCashOrGiftCardPaymentRequest request)
    {
        var order = await _orderRepository.Get(request.OrderId);

        if (order == null || (order.Status != OrderStatus.Open && order.Status != OrderStatus.PartiallyPaid))
        {
            return;
        } 
        
        var payment = new Payment
        {
            BusinessId = request.BusinessId,
            Method = request.PaymentMethod,
            State = PaymentState.Succeeded,
            Amount = request.PaymentAmount,
            Date = DateTime.UtcNow,
            OrderId = request.OrderId
        };
        
        order.Payments.Add(payment);
        
        await _paymentRepository.Create(payment);

        var orderTotal = order.CalculateTotalAmout();
        var orderPaid = order.CalculatePaidAmount();

        order.Status = orderPaid >= orderTotal ? OrderStatus.Closed : OrderStatus.PartiallyPaid;

        if (order.Status == OrderStatus.Closed)
        {
            order.Closed = DateTime.UtcNow;
        }
        
        await _unitOfWork.SaveChanges();
    }

    public async Task HandleStripeWebhook(HandleStripeWebhookRequest request)
    {
        if (request.Event.Type != EventTypes.CheckoutSessionCompleted &&
            request.Event.Type != EventTypes.CheckoutSessionExpired)
        {
            return;
        }
        
        var sessionJson = request.Event.Data.RawObject.ToString();
        var session = JsonConvert.DeserializeObject<Session>(sessionJson) as Session;
        
        var payment = await _paymentRepository.GetByCheckoutSessionId(session?.Id);

        if (payment == null)
        {
            return;
        }

        payment.State = request.Event.Type == EventTypes.CheckoutSessionCompleted ? PaymentState.Succeeded : PaymentState.Failed;

        var order = await _orderRepository.Get(payment.OrderId);
        
        var orderTotal = order.CalculateTotalAmout();
        var orderPaid = order.CalculatePaidAmount();

        order.Status = orderPaid >= orderTotal ? OrderStatus.Closed : OrderStatus.PartiallyPaid;

        if (order.Status == OrderStatus.Closed)
        {
            order.Closed = DateTime.UtcNow;
        }

        await _orderRepository.Update(order);
        await _paymentRepository.Update(payment);
        await _unitOfWork.SaveChanges();
    }
}