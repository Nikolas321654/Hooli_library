using System.ComponentModel.DataAnnotations;

namespace HomeLib.API.Model.DataRequest;

public class PlaylistRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;
}