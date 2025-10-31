namespace HomeLib.API.DataTypes.DataResponse;

public class AlbumResponse
{
    public Guid AlbumId { get; set; }
    public string Name { get; set; }
    public int Year { get; set; }
    public string? Artist { get; set; }
    public ICollection<TrackForAlbumResponse> Tracks { get; set; } = new List<TrackForAlbumResponse>(); 
}