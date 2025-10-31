namespace HomeLib.Core.Model;

public class Artist
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool Grammy { get; set; }

    public virtual ICollection<Album> Albums { get; set; } = new List<Album>();
    public virtual ICollection<Track> Tracks { get; set; } = new List<Track>();
}