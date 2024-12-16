using System.Text.Json.Serialization;
using PoS.WebApi.Application.Services.Reservation.Contracts;

namespace PoS.WebApi.Application.Services.Order.Contracts;

public class UpdateOrderReservationRequest
{
    [JsonIgnore]
    public Guid Id { get; set; }

    public UpdateReservationRequest Reservation { get; set; }
}