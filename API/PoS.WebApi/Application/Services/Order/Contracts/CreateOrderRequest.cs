using System.Text.Json.Serialization;
using PoS.WebApi.Application.Services.Reservation.Contracts;
using PoS.WebApi.Domain.Entities;

namespace PoS.WebApi.Application.Services.Order.Contracts;

public class CreateOrderRequest
{
    [JsonIgnore]
    public Guid BusinessId { get; set; }
    
    [JsonIgnore]
    public Guid EmployeeId { get; set; }

    public Guid? DiscountId { get; set; }

    public Guid? ServiceChargeId { get; set; }
    
    public CreateReservationRequest Reservation { get; set; }
    
    public IEnumerable<OrderItemDto> OrderItems { get; set; }
}