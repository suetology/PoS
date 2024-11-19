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
        var allGroups = await _itemGroupService.GetAllItemGroupsAsync(parameters);

        return Ok(allGroups);
    }

    [HttpGet]
    [Tags("Inventory")]
    [Route("{groupID}", Name = nameof(GetItemGroupById))]
    public async Task<IActionResult> GetItemGroupById([FromRoute] Guid groupID)
    {
        var itemGroup = await _itemGroupService.GetItemGroupByIdAsync(groupID);

        if (itemGroup == null)
        {
            return NotFound();
        }

        return Ok(itemGroup);
    }

    [Authorize(Roles = "SuperAdmin,BusinessOwner")]
    [HttpPost]
    [Route("item-Group")]
    [Tags("Inventory")]
    public async Task<IActionResult> CreateItemGroup([FromBody] ItemGroupDto itemGroupDto)
    {
        await _itemGroupService.CreateItemGroup(itemGroupDto);
        return NoContent();
    }

    [Authorize(Roles = "SuperAdmin,BusinessOwner")]
    [HttpPatch]
    [Route("{groupID}", Name = nameof(UpdateItemGroup))]
    [Tags("Inventory")]
    public async Task<IActionResult> UpdateItemGroup([FromRoute] Guid groupID, [FromBody] ItemGroupDto itemGroupDto)
    {
        await _itemGroupService.UpdateItemGroup(groupID, itemGroupDto);
        return NoContent();
    }
}
