namespace HomeLib.API.DataTypes.DataResponse;

public class TrackResponse
{
    public Guid TrackId { get; set; }
    public string Name { get; set; }
    public string? ArtistId { get; set; }
    public string? AlbumId { get; set; }
    public int Duration { get; set; }
}