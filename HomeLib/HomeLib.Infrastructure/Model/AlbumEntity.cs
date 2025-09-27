namespace HomeLib.Infrastructure.Model;

public class AlbumEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Year { get; set; }
    public Guid ArtistId { get; set; }

    public ArtistEntity Artist { get; set; } = null!;
    public ICollection<TrackEntity> Tracks { get; set; } = new List<TrackEntity>();
}