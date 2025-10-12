using HomeLib.API.DataTypes.DataRequest;
using HomeLib.API.DataTypes.DataResponse;
using HomeLib.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HomeLib.API.User;

[ApiController]
[Route("api/user")]
public class UserController(IUserService userService, ILogger<UserController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        try
        {
            var users = await userService.GetAllUsers();
            var userResponse = users.Select(user => new UserResponse()
            {
                UserId = user.Id,
                Login = user.Login,
                Name = user.Name,
            }).ToList();
            logger.LogInformation("Getting all users");
            return Ok(userResponse);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting users");
            return StatusCode(500, ex);
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        try
        {
            var user = await userService.GetUsersById(id);
            if (user == null) throw (new Exception("User not found."));

            var userResponse = new UserResponse()
            {
                UserId = user.Id,
                Name = user.Name,
                Login = user.Login,
            };
            logger.LogInformation("Getting user with id {Guid}", id);
            return Ok(userResponse);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, "Error getting user");
            return NotFound(ex.Message);
        }
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] UserRequest userRequest)
    {
        try
        {
            var userLogin = userService.GetUserByLogin(userRequest.Login);
            if (userLogin != null) throw new Exception("User already exists.");

            await userService.RegisterUser(userRequest.Login, userRequest.Password, userRequest.Name);
            logger.LogInformation("User with login {UserRequestLogin} registered", userRequest.Login);
            return Ok("User created");
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, "Error creating user");
            return StatusCode(400, ex.Message);
        }
    }


    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromBody] UserRequest userRequest)
    {
        try
        {
            var token = await userService.Login(userRequest.Login, userRequest.Password);
            logger.LogInformation("Login user {UserRequestLogin} logged in", userRequest.Login);
            return token == null ? throw new Exception("Invalid login or password") : Ok(token);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, "Error logging in");
            return StatusCode(400, ex.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        try
        {
            var user = await userService.GetUsersById(id);
            if (user == null) throw new Exception("User not found");

            await userService.DeleteUserAsync(id);
            logger.LogInformation("User with id {Guid} deleted", id);
            return Ok("User deleted");
        }
        catch (Exception ex)
        {
            logger.LogError("User with id {Guid} not found", id);
            return NotFound(ex.Message);
        }
    }
}