using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoS.WebApi.Application.Services.Item;
using PoS.WebApi.Application.Services.Item.Contracts;
using PoS.WebApi.Domain.Entities;
using PoS.WebApi.Domain.Enums;
using PoS.WebApi.Infrastructure.Security.Extensions;

namespace PoS.WebApi.Presentation.Controllers;

[ApiController]
[Route("inventory/item/{itemId}/variations")]
public class ItemVariationController : ControllerBase
{
    private readonly IItemService _itemService;

    public ItemVariationController(IItemService itemService)
    {
        _itemService = itemService;
    }

    [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)}")]
    [HttpPost]
    [Tags("Inventory")]
    public async Task<IActionResult> CreateItemVariation([FromRoute] Guid itemId, [FromBody] CreateItemVariationRequest request)
    {
        var businessId = User.GetBusinessId();

        if (businessId == null)
        {
            return Unauthorized("Failed to retrieve Business ID");
        }
        
        request.ItemId = itemId;
        request.BusinessId = businessId.Value;

        _itemService.CreateItemVariation(request);

        return NoContent();
    }

    [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)},{nameof(Role.Employee)}")]
    [HttpGet]
    [Tags("Inventory")]
    public async Task<IActionResult> GetAllItemVariations([FromRoute] Guid itemId)
    {
        var businessId = User.GetBusinessId();

        if (businessId == null)
        {
            return Unauthorized("Failed to retrieve Business ID");
        }

        var request = new GetAllItemVariationsRequest
        {
            ItemId = itemId,
            BusinessId = businessId.Value,
        };
        
        var response = await _itemService.GetAllItemVariations(request);

        return Ok(response);
    }

    [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)},{nameof(Role.Employee)}")]
    [HttpGet("{itemVariationId}")]
    [Tags("Inventory")]
    public async Task<IActionResult> GetItemVariation([FromRoute] Guid itemId, [FromRoute] Guid itemVariationId)
    {
        var businessId = User.GetBusinessId();

        if (businessId == null)
        {
            return Unauthorized("Failed to retrieve Business ID");
        }

        var request = new GetItemVariationRequest
        {
            Id = itemVariationId,
            ItemId = itemId,
            BusinessId = businessId.Value,
        };
        
        var response = _itemService.GetItemVariation(request);
        
        return Ok(response);
    }
}