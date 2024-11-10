using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoS.WebApi.Application.Services.Discount;
using PoS.WebApi.Application.Services.Discount.Contracts;
using PoS.WebApi.Application.Services.GroupDiscount;

namespace PoS.WebApi.Presentation.Controllers;
[ApiController]
[Route("discount")]

public class DiscountController : ControllerBase
{
    private readonly IDiscountService _discountService;
    private readonly IGroupDiscountService _groupDiscountService;

    public DiscountController(IDiscountService discountService, IGroupDiscountService groupDiscountService)
    {
        _discountService = discountService;
        _groupDiscountService = groupDiscountService;
    }

    [Authorize(Roles = "SuperAdmin,BusinessOwner")]
    [HttpPost(Name = nameof(CreateDiscount))]
    public async Task<IActionResult> CreateDiscount([FromBody] DiscountDto discountDto)
    {
        await _discountService.CreateDiscount(discountDto);

        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllDiscounts([FromQuery] QueryParameters parameters)
    {
        
        var allDiscounts = await _discountService.GetAllDiscounts(parameters);

        return Ok(allDiscounts);
    }

    [HttpGet]
    [Route("{discountId}", Name = nameof(GetDiscount))]
    public async Task<IActionResult> GetDiscount([FromRoute] Guid discountId)
    {
        var discount = await _discountService.GetDiscount(discountId);
        if (discount == null)
        {
            return NotFound();
        }

        return Ok(discount);
    }

    [Authorize(Roles = "SuperAdmin,BusinessOwner")]
    [HttpDelete]
    [Route("{discountId}", Name = nameof(DeleteDiscount))]
    public async Task<IActionResult> DeleteDiscount([FromRoute] Guid discountId)
    {
        await _discountService.DeleteDiscountById(discountId);
        return NoContent();
    }


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
}

