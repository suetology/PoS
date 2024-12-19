using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoS.WebApi.Application.Services.Item;
using PoS.WebApi.Application.Services.Item.Contracts;
using PoS.WebApi.Domain.Enums;
using PoS.WebApi.Infrastructure.Security.Extensions;

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

    [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)}")]
    [HttpPost]
    [Route("item")]
    [Tags("Inventory")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> CreateItem([FromBody] CreateItemRequest request)
    {
        var businessId = User.GetBusinessId();

        if (businessId == null)
        {
            return Unauthorized("Failed to retrieve Business ID");
        }

        request.BusinessId = businessId.Value;
        
        await _itemService.CreateItem(request);

        return NoContent();
    }

    [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)},{nameof(Role.Employee)}")]
    [HttpGet]
    [Route("item")]
    [Tags("Inventory")]
    [ProducesResponseType(typeof(GetAllItemsResponse),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllItems([FromQuery] QueryParameters parameters)
    {     
        var businessId = User.GetBusinessId();

        if (businessId == null)
        {
            return Unauthorized("Failed to retrieve Business ID");
        }

        var request = new GetAllItemsRequest
        {
            BusinessId = businessId.Value,
            QueryParameters = parameters
        };

        var response = await _itemService.GetAllItems(request);

        return Ok(response);
    }

    [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)},{nameof(Role.Employee)}")]
    [HttpGet]
    [Tags("Inventory")]
    [Route("item/{itemId}", Name = nameof(GetItem))]
    [ProducesResponseType(typeof(GetItemResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetItem([FromRoute] Guid itemId)
    {
        var businessId = User.GetBusinessId();

        if (businessId == null)
        {
            return Unauthorized("Failed to retrieve Business ID");
        }

        var request = new GetItemRequest
        {
            BusinessId = businessId.Value,
            ItemId = itemId
        };
        
        var response = await _itemService.GetItem(request);

        return Ok(response);
    }

    [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)}")]
    [HttpPatch]
    [Tags("Inventory")]
    [Route("item/{itemID}", Name = nameof(UpdateItem))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateItem([FromRoute] Guid itemID, [FromBody] UpdateItemRequest request)
    {
        var businessId = User.GetBusinessId();

        if (businessId == null)
        {
            return Unauthorized("Failed to retrieve Business ID");
        }

        request.Id = itemID;

        request.BusinessId = businessId.Value;

        await _itemService.UpdateItem(request);
        return NoContent();
    }
}
