namespace HomeLib.API.DataTypes.DataRequest;

public interface ITrackRequest
{
    string Name { get; set; }
    int Duration { get; set; }
    Guid ArtistId { get; set; }
    Guid AlbumId { get; set; }
}

public class TrackRequest : ITrackRequest
{
    public string Name { get; set; } = string.Empty;
    public int Duration { get; set; }
    public Guid ArtistId { get; set; }
    public Guid AlbumId { get; set; }
}