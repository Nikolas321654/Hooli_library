using HomeLib.API.DataTypes.DataResponse;

namespace HomeLib.API.Model.DataResponse;

public class ArtistTracksResponse
{
    public Guid ArtistId { get; set; }
    public string Name { get; set; }
    public bool Grammy { get; set; }
    public List<TrackForArtistResponse> Tracks { get; set; }
}