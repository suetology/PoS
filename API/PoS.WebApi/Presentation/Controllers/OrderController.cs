
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

        var employeeId = User.GetEmployeeId();

        if (employeeId == null)
        {
            return Unauthorized("Failed to retrieve Employee ID");
        }

        request.BusinessId = businessId.Value;
        request.EmployeeId = employeeId.Value;
        
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

    [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)},{nameof(Role.Employee)}")]
    [HttpPost("{orderId}/cancel")]
    public async Task<IActionResult> CancelOrder([FromRoute] Guid orderId)
    {
        var businessId = User.GetBusinessId();

        if (businessId == null)
        {
            return Unauthorized("Failed to retrieve Business ID");
        }

        var request = new CancelOrderRequest
        {
            Id = orderId,
            BusinessId = businessId.Value
        };

        await _orderService.CancelOrder(request);

        return NoContent();
    }

    [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)},{nameof(Role.Employee)}")]
    [HttpPost("{orderId}/tip")]
    public async Task<IActionResult> AddTip([FromRoute] Guid orderId, [FromBody] AddTipRequest request)
    {        
        var businessId = User.GetBusinessId();

        if (businessId == null)
        {
            return Unauthorized("Failed to retrieve Business ID");
        }

        request.OrderId = orderId;
        request.BusinessId = businessId.Value;

        await _orderService.AddTip(request);

        return NoContent();
    }
}