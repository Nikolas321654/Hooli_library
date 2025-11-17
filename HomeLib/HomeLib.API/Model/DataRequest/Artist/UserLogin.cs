using System.ComponentModel.DataAnnotations;

namespace HomeLib.API.DataTypes.DataRequest;

public class UserLogin
{
    [Required(ErrorMessage = "Name is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Login { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = string.Empty;
}