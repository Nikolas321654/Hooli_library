namespace HomeLib.API.DataTypes.DataRequest;

public interface IArtistRequest
{
    string Name { get; set; }
    bool Grammy { get; set; }
}

public class ArtistRequest : IArtistRequest
{
    public string Name { get; set; } = string.Empty;
    public bool Grammy { get; set; }
}