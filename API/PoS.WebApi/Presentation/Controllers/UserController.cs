using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoS.WebApi.Application.Services.User;
using PoS.WebApi.Application.Services.User.Contracts;
using PoS.WebApi.Domain.Enums;

namespace PoS.WebApi.Presentation.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)}")]
    [HttpPost(Name = nameof(CreateUser))]
    [Tags("User Management")]
    public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
    {
        await _userService.CreateUser(userDto);

        return NoContent();
    }

    [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)},{nameof(Role.Employee)}")]
    [HttpGet]
    [Tags("User Management")]
    public async Task<IActionResult> GetAllUsers([FromQuery] QueryParameters parameters)
    {
        if (!QueryParameters.AllowedSortFields.Contains(parameters.OrderBy.ToLower()))
        {
            return BadRequest("Invalid sorting field. Allowed fields are name, surname, username, email, dateOfEmployment, and role.");
        }

        var allUsers = await _userService.GetAllUsers(parameters);

        return Ok(allUsers);
    }

    [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)},{nameof(Role.Employee)}")]
    [HttpGet]
    [Tags("User Management")]
    [Route("{userId}", Name = nameof(GetUser))]
    public async Task<IActionResult> GetUser([FromRoute] Guid userId)
    {
        var user = await _userService.GetUser(userId);

        return Ok(user);
    }

    [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)}")]
    [HttpGet("roles", Name = nameof(GetAvailableRoles))]
    [Tags("User Management")]
    public async Task<IActionResult> GetAvailableRoles()
    {
        var roles = await _userService.GetAvailableRoles();

        return Ok(roles);
    }
}
