using System.ComponentModel.DataAnnotations;

namespace HomeLib.API.DataTypes.DataRequest;

public class UserRequest
{
    [Required(ErrorMessage = "Login is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    [MaxLength(40, ErrorMessage = "Max 40 Characters")]
    public string Login { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
    [MaxLength(12, ErrorMessage = "Password must be at most 12 characters long.")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Name is required")]
    [MinLength(3, ErrorMessage = "Name must be at least 3 characters long.")]
    [MaxLength(30, ErrorMessage = "Name must be at most 30 characters long.")]
    public string Name { get; set; } = string.Empty;
}