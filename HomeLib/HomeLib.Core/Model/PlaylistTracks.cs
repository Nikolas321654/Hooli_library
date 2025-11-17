using HomeLib.Core.Model;

namespace HomeLib.Core;

public sealed class PlaylistTracks
{
    public Guid TrackId { get; set; }
    public Guid PlaylistId { get; set; }
    public int Position { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public UserPlaylists UserPlaylists { get; set; } = null!;
    public Track Track { get; set; } = null!;
}