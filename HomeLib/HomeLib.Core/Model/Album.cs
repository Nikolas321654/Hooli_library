namespace HomeLib.Core;

public class Album
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Year { get; set; }
    public Guid ArtistId { get; set; }

    public Artist Artist { get; set; } = null!;
    public ICollection<Track> Tracks { get; set; } = new List<Track>();
}