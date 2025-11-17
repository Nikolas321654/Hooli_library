namespace HomeLib.Core.Model;

public sealed class Album
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Year { get; set; }
    public Guid ArtistId { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public Artist? Artist { get; set; }
    public ICollection<Track> Tracks { get; set; } = new List<Track>();
}