using System.ComponentModel.DataAnnotations;

namespace HomeLib.API.Model.DataRequest;

public class PlaylistRequest
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = string.Empty;
}