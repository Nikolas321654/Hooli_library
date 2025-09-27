namespace HomeLib.Infrastructure.Model;

public class TrackEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid ArtistId { get; set; }
    public Guid AlbumId { get; set; }
    public int Duration { get; set; }

    public virtual ArtistEntity Artist { get; set; } = null!;
    public virtual AlbumEntity Album { get; set; } = null!;
    public virtual ICollection<PlaylistTracksEntity> PlaylistTrack { get; set; } = new List<PlaylistTracksEntity>();
}