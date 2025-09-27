namespace HomeLib.Infrastructure.Model;

public class PlaylistTracksEntity
{
    public Guid TrackId { get; set; }
    public Guid PlaylistId { get; set; }
    public int Position { get; set; }
    public DateTime CreatedAt { get; set; }

    public virtual UserPlaylistsEntity UserPlaylists { get; set; } = null!;
    public virtual TrackEntity Track { get; set; } = null!;
}