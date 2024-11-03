using Microsoft.AspNetCore.Mvc;
using PoS.WebApi.Application.Services.Discount;
using PoS.WebApi.Application.Services.Discount.Contracts;

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
    [HttpPost(Name = nameof(CreateDiscount))]
    public async Task<IActionResult> CreateDiscount([FromBody] DiscountDto discountDto)
    {
        await _discountService.CreateDiscount(discountDto);

        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllDiscounts([FromQuery] QueryParameters parameters)
    {
        
        var allUsers = await _discountService.GetAllDiscounts(parameters);

        return Ok(allUsers);
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
}

