using System.Text.Json.Serialization;
using PoS.WebApi.Application.Services.Customer.Contracts;
using PoS.WebApi.Application.Services.Reservation.Contracts;

namespace PoS.WebApi.Application.Services.Order.Contracts;

public class CreateOrderRequest
{
    [JsonIgnore]
    public Guid BusinessId { get; set; }
    
    [JsonIgnore]
    public Guid EmployeeId { get; set; }

    public Guid? CustomerId { get; set; }

    public CreateCustomerRequest Customer { get; set; }

    public Guid? ServiceChargeId { get; set; }
    
    public CreateReservationRequest Reservation { get; set; }
    
    public IEnumerable<CreateOrderItemRequest> OrderItems { get; set; }
}