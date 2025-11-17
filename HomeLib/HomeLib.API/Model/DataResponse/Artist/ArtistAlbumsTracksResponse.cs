namespace HomeLib.API.DataTypes.DataResponse;

public class ArtistAlbumsTracksResponse
{
    public Guid ArtistId {get; set;} 
    public string Name { get; set; }
    public bool Grammy { get; set; }
    public List<AlbumForArtistResponse> Albums { get; set; }
    public List<TrackForArtistResponse> Tracks { get; set; }
}