namespace HomeLib.Core.Model;

public class Track
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid? ArtistId { get; set; }
    public Guid? AlbumId { get; set; }
    public int Duration { get; set; }

    public virtual Artist? Artist { get; set; }
    public virtual Album? Album { get; set; }
    public virtual ICollection<PlaylistTracks> PlaylistTrack { get; set; } = new List<PlaylistTracks>();
}