using System.Collections;

namespace PoS.WebApi.Application.Services.Order.Contracts;

public class GetAllOrdersResponse
{
    public IEnumerable<OrderDto> Orders { get; set; }
}