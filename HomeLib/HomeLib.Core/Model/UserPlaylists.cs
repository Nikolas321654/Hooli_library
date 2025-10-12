using HomeLib.Core.Model;

namespace HomeLib.Core;

public class UserPlaylists
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public Guid UserId { get; set; }

    public virtual User User { get; set; } = null!;
    public virtual ICollection<PlaylistTracks> PlaylistTracks { get; set; } = new List<PlaylistTracks>();
}