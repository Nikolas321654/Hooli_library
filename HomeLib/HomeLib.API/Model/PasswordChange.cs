using System.ComponentModel.DataAnnotations;

namespace HomeLib.API.DataTypes;

public class PasswordChange
{
    [Required]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
    [MaxLength(12, ErrorMessage = "Password must be at most 12 characters long.")]
    public required string NewPassword { get; set; }

    [Required]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
    public string OldPassword { get; set; }
}