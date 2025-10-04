using HomeLib.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HomeLib.API.User;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
}