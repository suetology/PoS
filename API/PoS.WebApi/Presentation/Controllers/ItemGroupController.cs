using Microsoft.AspNetCore.Mvc;
using PoS.WebApi.Application.Services.ItemGroup.Contracts;
using PoS.WebApi.Application.Services.ItemGroup;
using Microsoft.AspNetCore.Authorization;
using PoS.WebApi.Domain.Enums;
using PoS.WebApi.Infrastructure.Security.Extensions;

namespace PoS.WebApi.Presentation.Controllers;

[ApiController]
[Route("inventory")]
public class ItemGroupController : ControllerBase
{
    private readonly IItemGroupService _itemGroupService;

    public ItemGroupController(IItemGroupService itemGroupService)
    {
        _itemGroupService = itemGroupService;
    }

    [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)},{nameof(Role.Employee)}")]
    [HttpGet]
    [Route("item-Group")]
    [Tags("Inventory")]
    public async Task<IActionResult> GetAllItemGroups([FromQuery] QueryParameters parameters)
    {
        var businessId = User.GetBusinessId();

        if (businessId == null)
        {
            return Unauthorized("Failed to retrieve Business ID");
        }

        var request = new GetAllItemGroupsRequest
        {
            BusinessId = businessId.Value,
            QueryParameters = parameters
        };
        
        var response = await _itemGroupService.GetAllItemGroupsAsync(request);

        return Ok(response);
    }

    [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)},{nameof(Role.Employee)}")]
    [HttpGet]
    [Tags("Inventory")]
    [Route("{groupId}", Name = nameof(GetItemGroupById))]
    public async Task<IActionResult> GetItemGroupById([FromRoute] Guid groupId)
    {
        var businessId = User.GetBusinessId();

        if (businessId == null)
        {
            return Unauthorized("Failed to retrieve Business ID");
        }

        var request = new GetItemGroupRequest
        {
            Id = groupId,
            BusinessId = businessId.Value
        };
        
        var response = await _itemGroupService.GetItemGroupByIdAsync(request);

        if (response == null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)}")]
    [HttpPost]
    [Route("item-Group")]
    [Tags("Inventory")]
    public async Task<IActionResult> CreateItemGroup([FromBody] CreateItemGroupRequest request)
    {
        var businessId = User.GetBusinessId();

        if (businessId == null)
        {
            return Unauthorized("Failed to retrieve Business ID");
        }

        request.BusinessId = businessId.Value;
        
        await _itemGroupService.CreateItemGroup(request);
        return NoContent();
    }

    [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)}")]
    [HttpPatch]
    [Route("{groupID}", Name = nameof(UpdateItemGroup))]
    [Tags("Inventory")]
    public async Task<IActionResult> UpdateItemGroup([FromRoute] Guid groupID, [FromBody] UpdateItemGroupRequest request)
    {
        var businessId = User.GetBusinessId();

        if (businessId == null)
        {
            return Unauthorized("Failed to retrieve Business ID");
        }

        request.BusinessId = businessId.Value;
        
        await _itemGroupService.UpdateItemGroup(request);
        return NoContent();
    }
}
