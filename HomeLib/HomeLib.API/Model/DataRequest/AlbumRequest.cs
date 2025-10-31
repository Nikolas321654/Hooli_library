namespace HomeLib.API.Model.DataRequest;

public class AlbumRequest
{
    public string Name { get; set; }
    public int Year { get; set; }
    public Guid ArtistId { get; set; }
}