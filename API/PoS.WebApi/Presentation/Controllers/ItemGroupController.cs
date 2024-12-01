using Microsoft.AspNetCore.Mvc;
using PoS.WebApi.Application.Services.ItemGroup.Contracts;
using PoS.WebApi.Application.Services.ItemGroup;
using Microsoft.AspNetCore.Authorization;

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

    [HttpGet]
    [Route("item-Group")]
    [Tags("Inventory")]
    public async Task<IActionResult> GetAllItemGroups([FromQuery] QueryParameters parameters)
    {
        var response = await _itemGroupService.GetAllItemGroupsAsync(parameters);

        return Ok(response);
    }

    [HttpGet]
    [Tags("Inventory")]
    [Route("{groupId}", Name = nameof(GetItemGroupById))]
    public async Task<IActionResult> GetItemGroupById([FromRoute] Guid groupId)
    {
        var response = await _itemGroupService.GetItemGroupByIdAsync(groupId);

        if (response == null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [Authorize(Roles = "SuperAdmin,BusinessOwner")]
    [HttpPost]
    [Route("item-Group")]
    [Tags("Inventory")]
    public async Task<IActionResult> CreateItemGroup([FromBody] CreateItemGroupRequest request)
    {
        await _itemGroupService.CreateItemGroup(request);
        return NoContent();
    }

    [Authorize(Roles = "SuperAdmin,BusinessOwner")]
    [HttpPatch]
    [Route("{groupID}", Name = nameof(UpdateItemGroup))]
    [Tags("Inventory")]
    public async Task<IActionResult> UpdateItemGroup([FromRoute] Guid groupID, [FromBody] UpdateItemGroupRequest request)
    {
        await _itemGroupService.UpdateItemGroup(groupID, request);
        return NoContent();
    }
}
