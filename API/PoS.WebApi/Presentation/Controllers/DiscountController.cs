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
        if (response == null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)}")]
    [HttpDelete]
    [Route("{discountId}", Name = nameof(DeleteDiscount))]
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
    
    /*
    // GROUP DISCOUNTS
    [Authorize(Roles = "SuperAdmin,BusinessOwner")]
    [HttpPost("{discountId}/assign-group/{groupId}")]
    public async Task<IActionResult> AssignDiscountToGroup([FromRoute] Guid discountId, [FromRoute] Guid groupId)
    {
        await _groupDiscountService.AssignDiscountToGroupAsync(discountId, groupId);
        return NoContent();
    }

    [Authorize(Roles = "SuperAdmin,BusinessOwner")]
    [HttpDelete("{discountId}/remove-group/{groupId}")]
    public async Task<IActionResult> RemoveDiscountFromGroup([FromRoute] Guid discountId, [FromRoute] Guid groupId)
    {
        await _groupDiscountService.RemoveDiscountFromGroupAsync(discountId, groupId);
        return NoContent();
    }

    [HttpGet("groupDiscount")]
    public async Task<IActionResult> GetAllGroups()
    {
        var groupDiscounts = await _groupDiscountService.GetAllGroups();
        return Ok(groupDiscounts);
    }

    // SINGLE ITEM DISCOUNT

    [Authorize(Roles = "SuperAdmin,BusinessOwner")]
    [HttpPost("{discountId}/assign-item/{itemId}")]
    public async Task<IActionResult> AssignDiscountToItem([FromRoute] Guid discountId, [FromRoute] Guid itemId)
    {
        await _itemDiscountService.AssignDiscountToItemAsync(discountId, itemId);
        return NoContent();
    }
    */
}

