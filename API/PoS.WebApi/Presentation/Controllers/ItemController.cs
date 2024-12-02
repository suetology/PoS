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

    [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)},{nameof(Role.Employee)}")]
    [HttpPost]
    [Route("item")]
    [Tags("Inventory")]
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
    public async Task<IActionResult> GetAllItems([FromQuery] QueryParameters parameters)
    {
        //if (!QueryParameters.AllowedSortFields.Contains(parameters.OrderBy.ToLower()))
        //{
        //    return BadRequest("Invalid sorting field. Allowed fields are name, surname, username, email, dateOfEmployment, and role.");
        //}
        
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
}
