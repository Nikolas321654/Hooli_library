namespace HomeLib.API.DataTypes.DataResponse;

public class TrackResponse
{
    public Guid TrackId { get; set; }
    public string Name { get; set; }
    public string? ArtistName { get; set; }
    public string? AlbumName { get; set; }
    public int Duration { get; set; }
}