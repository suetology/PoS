using Amazon.SimpleNotificationService.Model;
using PoS.WebApi.Application.Repositories;
using PoS.WebApi.Application.Services.Refund.Contracts;
using PoS.WebApi.Domain.Common;
using PoS.WebApi.Domain.Entities;
using PoS.WebApi.Domain.Enums;
using Stripe;
using Stripe.Checkout;

namespace PoS.WebApi.Application.Services.Refund;

public class RefundService : IRefundService
{
    private readonly IRefundRepository _refundRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RefundService(
        IRefundRepository refundRepository,
        IOrderRepository orderRepository,
        IUnitOfWork unitOfWork)
    {
        _refundRepository = refundRepository;
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task CreateRefund(CreateRefundRequest request)
    {
        var order = await _orderRepository.Get(request.OrderId);

        if (order == null || order.BusinessId != request.BusinessId)
        {
            throw new NotFoundException("Order not found");
        }

        if (order.Status != OrderStatus.PartiallyPaid && order.Status != OrderStatus.Closed)
        {
            throw new ArgumentException("To refund an order it should be either Closed or PartiallyPaid");
        }

        var succeededPayments = order.Payments.Where(p => p.State == PaymentState.Succeeded);

        foreach (var payment in succeededPayments) 
        {
            await RefundPayment(payment);
        }

        order.Status = OrderStatus.Refunded;

        var refund = new Domain.Entities.Refund
        {
            BusinessId = request.BusinessId,
            Reason = request.Reason,
            OrderId = request.OrderId,
            Amount = order.CalculatePaidAmount(),
            Date = DateTime.UtcNow
        };

        await _refundRepository.Create(refund);
        await _orderRepository.Update(order);
        await _unitOfWork.SaveChanges();
    }

    public async Task<GetAllRefundsResponse> GetAllRefunds(GetAllRefundsRequest request)
    {
        var refunds = await _refundRepository.GetAll();
        var refundDtos = refunds
            .Where(r => r.BusinessId == request.BusinessId)
            .Select(r => new RefundDto
            {
                Id = r.Id,
                Amount = r.Amount,
                Date = r.Date,
                Reason = r.Reason,
                OrderId = r.OrderId
            });

        return new GetAllRefundsResponse
        {
            Refunds = refundDtos
        };
    }

    private async Task RefundPayment(Payment payment)
    {
        if (payment.Method == Domain.Enums.PaymentMethod.CreditOrDebitCard)
        {
            var sessionService = new SessionService();
            var session = sessionService.Get(payment.StripeCheckoutSessionId);
            var paymentIntentId = session?.PaymentIntentId;
            
            var refundOptions = new RefundCreateOptions
            {
                PaymentIntent = paymentIntentId
            };

            var refundService = new Stripe.RefundService();
            await refundService.CreateAsync(refundOptions);
        }        

        payment.State = PaymentState.Refunded;
    } 
}