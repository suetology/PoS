using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoS.WebApi.Application.Services.Discount;
using PoS.WebApi.Application.Services.Discount.Contracts;
using PoS.WebApi.Domain.Enums;
using PoS.WebApi.Infrastructure.Security.Extensions;

namespace PoS.WebApi.Presentation.Controllers;
[ApiController]
[Route("discount")]

public class DiscountController : ControllerBase
{
    private readonly IDiscountService _discountService;

    public DiscountController(IDiscountService discountService)
    {
        _discountService = discountService;
    }

    [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)}")]
    [HttpPost(Name = nameof(CreateDiscount))]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> CreateDiscount([FromBody] CreateDiscountRequest request)
    {
        var businessId = User.GetBusinessId();

        if (businessId == null)
        {
            return Unauthorized("Failed to retrieve Business ID");
        }

        request.BusinessId = businessId.Value;
        
        await _discountService.CreateDiscount(request);

        return NoContent();
    }

    [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)},{nameof(Role.Employee)}")]
    [HttpGet]
    [ProducesResponseType(typeof(GetAllDiscountsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllDiscounts([FromQuery] QueryParameters parameters)
    {
        var businessId = User.GetBusinessId();

        if (businessId == null)
        {
            return Unauthorized("Failed to retrieve Business ID");
        }

        var request = new GetAllDiscountsRequest
        {
            BusinessId = businessId.Value,
            QueryParameters = parameters
        };
        
        var response = await _discountService.GetAllDiscounts(request);

        return Ok(response);
    }

    [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)},{nameof(Role.Employee)}")]
    [HttpGet]
    [ProducesResponseType(typeof(GetDiscountResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Route("{discountId}", Name = nameof(GetDiscount))]
    public async Task<IActionResult> GetDiscount([FromRoute] Guid discountId)
    {
        var businessId = User.GetBusinessId();

        if (businessId == null)
        {
            return Unauthorized("Failed to retrieve Business ID");
        }

        var request = new GetDiscountRequest
        {
            Id = discountId,
            BusinessId = businessId.Value
        };
        
        var response = await _discountService.GetDiscount(request);

        return Ok(response);
    }

    [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)}")]
    [HttpDelete]
    [Route("{discountId}", Name = nameof(DeleteDiscount))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteDiscount([FromRoute] Guid discountId)
    {
        var businessId = User.GetBusinessId();

        if (businessId == null)
        {
            return Unauthorized("Failed to retrieve Business ID");
        }

        var request = new DeleteDiscountRequest
        {
            Id = discountId,
            BusinessId = businessId.Value
        };
        
        await _discountService.DeleteDiscountById(request);

        return NoContent();
    }
}

