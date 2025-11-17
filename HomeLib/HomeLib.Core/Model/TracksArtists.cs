namespace HomeLib.Core.Model;

public sealed class TracksArtists
{
    public Guid TrackId { get; set; }
    public Guid ArtistId { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public Track? Track { get; set; }
    public Artist? Artist { get; set; }
}