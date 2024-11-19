using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoS.WebApi.Application.Services.Item;
using PoS.WebApi.Application.Services.Item.Contracts;

namespace PoS.WebApi.Presentation.Controllers;

[ApiController]
[Route("inventory")]
public class ItemController : ControllerBase
{
    private readonly IItemService _itemService;

    public ItemController(IItemService itemService)
    {
        _itemService = itemService;
    }

    //[Authorize(Roles = "SuperAdmin,BusinessOwner,Employee")]
    [HttpPost]
    [Route("item")]
    [Tags("Inventory")]
    public async Task<IActionResult> CreateItem([FromBody] ItemDto itemDto)
    {
        await _itemService.CreateItem(itemDto);

        return NoContent();
    }

    [HttpGet]
    [Route("item")]
    [Tags("Inventory")]
    public async Task<IActionResult> GetAllItems([FromQuery] QueryParameters parameters)
    {
        //if (!QueryParameters.AllowedSortFields.Contains(parameters.OrderBy.ToLower()))
        //{
        //    return BadRequest("Invalid sorting field. Allowed fields are name, surname, username, email, dateOfEmployment, and role.");
        //}

        var allItems = await _itemService.GetAllItems(parameters);

        return Ok(allItems);
    }

    [HttpGet]
    [Tags("Inventory")]
    [Route("item/{itemId}", Name = nameof(GetItem))]
    public async Task<IActionResult> GetItem([FromRoute] Guid itemId)
    {
        var item = await _itemService.GetItem(itemId);

        return Ok(item);
    }
}
