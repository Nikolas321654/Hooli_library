namespace HomeLib.Core.Model;

public sealed class Artist
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool Grammy { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public ICollection<TracksArtists> TrackArtists { get; set; } = new List<TracksArtists>();
    public ICollection<Album> Albums { get; set; } = new List<Album>();
}