using HomeLib.Core.Model;

namespace HomeLib.Core;

public class PlaylistTracks
{
    public Guid TrackId { get; set; }
    public Guid PlaylistId { get; set; }
    public int Position { get; set; }
    public DateTime CreatedAt { get; set; }

    public virtual UserPlaylists UserPlaylists { get; set; } = null!;
    public virtual Track Track { get; set; } = null!;
}