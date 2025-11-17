namespace HomeLib.Core.Model;

public sealed class Track
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid? AlbumId { get; set; }
    public int Duration { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public Album? Album { get; set; }
    public ICollection<TracksArtists> TrackArtists { get; set; } = new List<TracksArtists>();
    public ICollection<PlaylistTracks> PlaylistTrack { get; set; } = new List<PlaylistTracks>();
}