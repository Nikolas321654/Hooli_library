using System.ComponentModel.DataAnnotations;

namespace HomeLib.API.Model.DataRequest;

public class AlbumRequest
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Year is required")]
    public int Year { get; set; }

    [Required(ErrorMessage = "Year is required")]
    public Guid ArtistId { get; set; }
}