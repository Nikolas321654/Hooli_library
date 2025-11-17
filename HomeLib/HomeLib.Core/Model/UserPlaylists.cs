namespace HomeLib.Core.Model;

public sealed class UserPlaylists
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid UserId { get; set; }

    public User User { get; set; } = null!;
    public ICollection<PlaylistTracks> PlaylistTracks { get; set; } = new List<PlaylistTracks>();
}