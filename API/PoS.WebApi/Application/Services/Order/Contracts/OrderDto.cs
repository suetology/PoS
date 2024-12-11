
namespace PoS.WebApi.Application.Services.Order.Contracts;

using PoS.WebApi.Application.Services.Customer.Contracts;
using PoS.WebApi.Application.Services.Discount.Contracts;
using PoS.WebApi.Application.Services.Reservation.Contracts;
using PoS.WebApi.Application.Services.ServiceCharge.Contracts;
using PoS.WebApi.Application.Services.User.Contracts;
using PoS.WebApi.Domain.Enums;

public class OrderDto 
{
    public Guid Id { get; set; }
    
    public UserDto Employee { get; set; }

    public OrderStatus Status { get; set; }

    public ServiceChargeDto ServiceCharge { get; set; }

    public decimal TipAmount { get; set; }

    public decimal FinalAmount { get; set; }

    public decimal PaidAmount { get; set; }

    public DateTime Created { get; set; }

    public DateTime? Closed { get; set; }

    public CustomerDto Customer { get; set; }

    public IEnumerable<OrderItemDto> OrderItems { get; set; }

    public ReservationDto Reservation { get; set; }

    // add paymentDtos
    // add refundDto
    // add discountDto
}