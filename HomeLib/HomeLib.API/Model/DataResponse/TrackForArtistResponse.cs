namespace HomeLib.API.DataTypes.DataResponse;

public class TrackForArtistResponse
{
    public Guid TrackId { get; set; }
    public string Name { get; set; }
    public string? Album { get; set; }
    public int Duration { get; set; }
}