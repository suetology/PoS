using Microsoft.AspNetCore.Mvc;
using PoS.WebApi.Application.Services.User;
using PoS.WebApi.Application.Services.User.Contracts;

namespace PoS.WebApi.Presentation.Controllers;

[ApiController]
[Route("user")]
public class UserController :ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var allUsers = await _userService.GetAllUsers();

        return Ok(allUsers);
    }

    [HttpPost(Name = nameof(CreateUser))]
    public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
    {
        await _userService.CreateUser(userDto);

        return NoContent();
    }
}
