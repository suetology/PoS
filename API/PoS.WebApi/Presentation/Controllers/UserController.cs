using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoS.WebApi.Application.Services.User;
using PoS.WebApi.Application.Services.User.Contracts;
using PoS.WebApi.Domain.Enums;
using PoS.WebApi.Infrastructure.Security.Extensions;

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
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        var businessId = User.GetBusinessId();

        if (businessId == null)
        {
            return Unauthorized("Failed to retrieve Business ID");
        }

        request.BusinessId = businessId.Value;
        
        await _userService.CreateUser(request);

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
        
        var businessId = User.GetBusinessId();

        if (businessId == null)
        {
            return Unauthorized("Failed to retrieve Business ID");
        }

        var request = new GetAllUsersRequest
        {
            BusinessId = businessId.Value,
            QueryParameters = parameters
        };

        var response = await _userService.GetAllUsers(request);

        return Ok(response);
    }

    [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)},{nameof(Role.Employee)}")]
    [HttpGet]
    [Tags("User Management")]
    [Route("{userId}", Name = nameof(GetUser))]
    public async Task<IActionResult> GetUser([FromRoute] Guid userId)
    {
        var businessId = User.GetBusinessId();

        if (businessId == null)
        {
            return Unauthorized("Failed to retrieve Business ID");
        }

        var request = new GetUserRequest
        {
            Id = userId,
            BusinessId = businessId.Value
        };
        
        var response = await _userService.GetUser(request);

        return Ok(response);
    }


    [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)}")]
    [Tags("User Management")]
    [HttpPatch("{userId}", Name = nameof(UpdateUser))]
    public async Task<IActionResult> UpdateUser([FromRoute] Guid userId, [FromBody] UpdateUserRequest request)
    {
        var businessId = User.GetBusinessId();

        if (businessId == null)
        {
            return Unauthorized("Failed to retrieve Business ID");
        }

        request.BusinessId = businessId.Value;
        request.Id = userId;

        var sucess = await _userService.UpdateUser(request);

        if (!sucess)
        {
            return NotFound();
        }

        return NoContent();
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
