namespace HomeLib.API.DataTypes.DataResponse;

public class PlaylistResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}