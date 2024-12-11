
namespace PoS.WebApi.Application.Services.Order.Contracts;

using PoS.WebApi.Application.Services.Customer.Contracts;
using PoS.WebApi.Application.Services.Reservation.Contracts;
using PoS.WebApi.Application.Services.ServiceCharge.Contracts;
using PoS.WebApi.Domain.Enums;

public class OrderDto 
{
    public Guid Id { get; set; }
    
    public Guid EmployeeId { get; set; }

    public OrderStatus Status { get; set; }

    public Guid? DiscountId { get; set; }

    public decimal DiscountAmount { get; set; }

    public ServiceChargeDto ServiceCharge { get; set; }

    public decimal TipAmount { get; set; }

    public decimal FinalAmount { get; set; }

    public decimal PaidAmount { get; set; }

    public DateTime Created { get; set; }

    public DateTime? Closed { get; set; }

    public CustomerDto Customer { get; set; }

    public IEnumerable<OrderItemDto> OrderItems { get; set; }

    public ReservationDto Reservation { get; set; }
}