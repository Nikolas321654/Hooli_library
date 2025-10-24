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
    [Authorize]
    [HttpGet]
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
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var user = await userService.GetUserById(id);
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
        var newUserID = await userService.RegisterUser(userRequest.Login, userRequest.Password, userRequest.Name);
        var token = await userService.Login(userRequest.Login, userRequest.Password);
        return Created($"api/user/{newUserID}", new { token = token });
    }


    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromBody] UserLogin UserLogin)
    {
        var token = await userService.Login(UserLogin.Login, UserLogin.Password);
        return Ok(token);
    }
    
    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        await userService.DeleteUserAsync(id);
        return NoContent();
    }

    [Authorize]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] PasswordChange passwordChange)
    {
        await userService.UpdateUserPassword(id, passwordChange.NewPassword, passwordChange.OldPassword);
        return NoContent();
    }
}