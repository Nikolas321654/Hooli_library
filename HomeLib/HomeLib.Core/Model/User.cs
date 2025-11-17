namespace HomeLib.Core.Model;

public sealed class User
{
    public Guid Id { get; set; }
    public string Login { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public int Version { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public ICollection<UserPlaylists> UserPlaylist { get; set; } = new List<UserPlaylists>();
}