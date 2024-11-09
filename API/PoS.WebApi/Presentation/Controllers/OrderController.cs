
namespace PoS.WebApi.Presentation.Controllers;

using Microsoft.AspNetCore.Mvc;
using PoS.WebApi.Application.Services.Order;
using PoS.WebApi.Application.Services.Order.Contracts;

[ApiController]
[Route("orders")]

public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] OrderDto orderDto)
    {
        await _orderService.CreateOrder(orderDto);

        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOrders([FromQuery] OrderQueryParameters parameters)
    {
        var orders = await _orderService.GetAllOrders(parameters);

        return Ok(orders);
    }

    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetOrder([FromRoute] Guid orderId)
    {
        var order = await _orderService.GetOrder(orderId);
        if (null == order) {
            return NotFound();
        }
        return Ok(order);
    }
}