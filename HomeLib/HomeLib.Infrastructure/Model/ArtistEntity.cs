namespace HomeLib.Infrastructure.Model;

public class ArtistEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool Grammy { get; set; }

    public virtual ICollection<AlbumEntity> Albums { get; set; } = new List<AlbumEntity>();
    public virtual ICollection<TrackEntity> Tracks { get; set; } = new List<TrackEntity>();
}