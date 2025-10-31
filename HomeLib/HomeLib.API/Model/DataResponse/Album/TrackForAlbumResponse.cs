namespace HomeLib.API.DataTypes.DataResponse;

public class TrackForAlbumResponse
{
    public Guid TrackId { get; set; }
    public string Name { get; set; }
    public int Duration { get; set; }
}