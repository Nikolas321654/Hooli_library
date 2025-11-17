using HomeLib.API.DataTypes.DataResponse;

namespace HomeLib.API.Model.DataRequest;

public class PlaylistWithTracksResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public List<PlaylistTrackResponse> Tracks { get; set; } = new List<PlaylistTrackResponse>();
}