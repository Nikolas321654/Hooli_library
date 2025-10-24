using System.ComponentModel.DataAnnotations;

namespace HomeLib.API.DataTypes.DataRequest;

public class ArtistRequest
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = string.Empty;

    public bool Grammy { get; set; }
}