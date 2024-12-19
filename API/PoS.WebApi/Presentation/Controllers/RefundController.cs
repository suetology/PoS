using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoS.WebApi.Application.Services.Refund;
using PoS.WebApi.Application.Services.Refund.Contracts;
using PoS.WebApi.Domain.Enums;
using PoS.WebApi.Infrastructure.Security.Extensions;

namespace PoS.WebApi.Presentation.Controllers;

[ApiController]
[Route("refunds")]
public class RefundController : ControllerBase
{
    private readonly IRefundService _refundService;

    public RefundController(IRefundService refundService)
    {
        _refundService = refundService;
    }

    [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)}")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateRefund(CreateRefundRequest request)
    {
        var businessId = User.GetBusinessId();

        if (businessId == null)
        {
            return Unauthorized("Failed to retrieve Business ID");
        }

        request.BusinessId = businessId.Value;

        await _refundService.CreateRefund(request);

        return NoContent();
    }

    [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)}")]
    [HttpGet]
    [ProducesResponseType(typeof(GetAllRefundsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllRefunds()
    {
        var businessId = User.GetBusinessId();

        if (businessId == null)
        {
            return Unauthorized("Failed to retrieve Business ID");
        }

        var request = new GetAllRefundsRequest
        {
            BusinessId = businessId.Value
        };

        var response = await _refundService.GetAllRefunds(request);

        return Ok(response);
    }
}