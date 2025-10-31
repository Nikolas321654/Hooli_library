namespace HomeLib.API.DataTypes.DataResponse;

public class AlbumForArtistResponse
{
    public string Name { get; set; }
    public int Year { get; set; }
    public Guid AlbumId { get; set; }
    public ICollection<TrackForAlbumResponse> Tracks { get; set; } = new List<TrackForAlbumResponse>(); 
}