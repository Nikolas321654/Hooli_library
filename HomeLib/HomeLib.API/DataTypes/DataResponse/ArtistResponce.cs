namespace HomeLib.API.DataTypes.DataResponse;

public interface IArtistResponce
{
    string Name { get; set; }
    bool Grammy { get; set; }
}

public class ArtistResponce : IArtistResponce
{
    public string Name { get; set; }
    public bool Grammy { get; set; }
}