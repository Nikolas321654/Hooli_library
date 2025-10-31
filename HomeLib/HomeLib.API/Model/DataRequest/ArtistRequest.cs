using System.ComponentModel.DataAnnotations;

namespace HomeLib.API.DataTypes.DataRequest;

public class ArtistRequest
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Grammy is required")]
    public bool Grammy { get; set; }
}