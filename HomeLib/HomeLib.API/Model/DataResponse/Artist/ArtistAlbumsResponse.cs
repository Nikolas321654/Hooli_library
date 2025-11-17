using HomeLib.API.DataTypes.DataResponse;

namespace HomeLib.API.Model.DataResponse;

public class ArtistAlbumsResponse
{
    public Guid ArtistId { get; set; }
    public string Name { get; set; }
    public bool Grammy { get; set; }
    public List<AlbumForArtistResponse> Albums { get; set; }
}