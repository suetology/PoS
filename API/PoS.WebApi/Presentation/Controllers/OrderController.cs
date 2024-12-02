
using Microsoft.AspNetCore.Authorization;
using PoS.WebApi.Domain.Enums;
using PoS.WebApi.Infrastructure.Security.Extensions;

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

    [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)},{nameof(Role.Employee)}")]
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
    {
        var businessId = User.GetBusinessId();

        if (businessId == null)
        {
            return Unauthorized("Failed to retrieve Business ID");
        }

        request.BusinessId = businessId.Value;
        
        await _orderService.CreateOrder(request);

        return NoContent();
    }

    [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)},{nameof(Role.Employee)}")]
    [HttpGet]
    public async Task<IActionResult> GetAllOrders([FromQuery] OrderQueryParameters parameters)
    {
        var businessId = User.GetBusinessId();

        if (businessId == null)
        {
            return Unauthorized("Failed to retrieve Business ID");
        }

        var request = new GetAllOrdersRequest
        {
            BusinessId = businessId.Value,
            QueryParameters = parameters
        };
        
        var response = await _orderService.GetAllOrders(request);

        return Ok(response);
    }

    [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)},{nameof(Role.Employee)}")]
    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetOrder([FromRoute] Guid orderId)
    {
        var businessId = User.GetBusinessId();

        if (businessId == null)
        {
            return Unauthorized("Failed to retrieve Business ID");
        }

        var request = new GetOrderRequest
        {
            Id = orderId,
            BusinessId = businessId.Value
        };
        
        var response = await _orderService.GetOrder(request);
        if (response == null) {
            return NotFound();
        }
        return Ok(response);
    }
}