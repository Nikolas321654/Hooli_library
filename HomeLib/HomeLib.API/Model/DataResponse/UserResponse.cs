namespace HomeLib.API.DataTypes.DataResponse;

public class UserResponse
{
    public Guid UserId { get; set; }
    public string Login { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
