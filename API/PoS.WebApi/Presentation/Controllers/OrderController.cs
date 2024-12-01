
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
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
    {
        await _orderService.CreateOrder(request);

        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOrders([FromQuery] OrderQueryParameters parameters)
    {
        var response = await _orderService.GetAllOrders(parameters);

        return Ok(response);
    }

    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetOrder([FromRoute] Guid orderId)
    {
        var response = await _orderService.GetOrder(orderId);
        if (response == null) {
            return NotFound();
        }
        return Ok(response);
    }
}