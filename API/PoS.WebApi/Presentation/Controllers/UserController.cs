using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoS.WebApi.Application.Services.User;
using PoS.WebApi.Application.Services.User.Contracts;

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

    [Authorize(Roles = "SuperAdmin,BusinessOwner")]
    [HttpPost(Name = nameof(CreateUser))]
    [Tags("User Management")]
    public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
    {
        await _userService.CreateUser(userDto);

        return NoContent();
    }

    [Authorize(Roles = "SuperAdmin,BusinessOwner,Employee")]
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

    [Authorize(Roles = "SuperAdmin,BusinessOwner,Employee")]
    [HttpGet]
    [Tags("User Management")]
    [Route("{userId}", Name = nameof(GetUser))]
    public async Task<IActionResult> GetUser([FromRoute] Guid userId)
    {
        var user = await _userService.GetUser(userId);

        return Ok(user);
    }

    //[HttpPatch("{userId}", Name = nameof(UpdateUser))]
    //public async Task<IActionResult> UpdateUser([FromRoute] Guid userId, [FromBody] JsonPatchDocument<User> user)
    //{
    //    var post = await _userService.GetUser(userId);

    //    if (post is null)
    //    {
    //        return NotFound();
    //    }

    //    user.ApplyTo(post);
    //    await _userService.EditAsync(post);

    //    return NoContent();
    //}
}
