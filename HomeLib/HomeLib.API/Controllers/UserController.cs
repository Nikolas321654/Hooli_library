using System.Security.Claims;
using System.Text.Json;
using HomeLib.API.DataTypes;
using HomeLib.API.DataTypes.DataRequest;
using HomeLib.API.DataTypes.DataResponse;
using HomeLib.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeLib.API.User;

[ApiController]
[Route("api/user")]
public class UserController(IUserService userService) : ControllerBase
{
    private Guid GetUserId()
    {
        var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return id == null ? throw new Exception("User not found") : Guid.Parse(id);
    }

    [Authorize]
    [HttpGet("all_users")]
    public async Task<IActionResult> GetUsers()
    {
        var users = await userService.GetAllUsers();
        var userResponse = users.Select(user => new UserResponse()
        {
            UserId = user.Id,
            Login = user.Login,
            Name = user.Name,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt,
        }).ToList();

        return Ok(userResponse);
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetUserById()
    {
        var user = await userService.GetUserById(GetUserId());
        var userResponse = new UserResponse()
        {
            UserId = user.Id,
            Name = user.Name,
            Login = user.Login,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt,
        };

        return Ok(userResponse);
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] UserRequest userRequest)
    {
        var newUserId = await userService.RegisterUser(userRequest.Login, userRequest.Password, userRequest.Name);
        var token = await userService.Login(userRequest.Login, userRequest.Password);
        
        return Created($"api/user/{newUserId}", new { BearerToken = token });
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromBody] UserLogin userLogin)
    {
        var token = await userService.Login(userLogin.Login, userLogin.Password);
        return Ok(new { BearerToken = token });
    }

    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> DeleteUser()
    {
        await userService.DeleteUserAsync(GetUserId());
        return NoContent();
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromBody] PasswordChange passwordChange)
    {
        await userService.UpdateUserPassword(GetUserId(), passwordChange.NewPassword, passwordChange.OldPassword);
        return Ok("Password changed");
    }
}