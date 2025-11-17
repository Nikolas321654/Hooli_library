using System.ComponentModel.DataAnnotations;

namespace HomeLib.API.DataTypes.DataRequest;

public class TrackRequest
{
    [Required(ErrorMessage = "Name is required")]
    [MaxLength(30, ErrorMessage = "Name must be no more at 30 characters long.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Durations is required")]
    [Range(1, 3600, ErrorMessage = "Duration must be between 1 and 3600 seconds")]
    public int Duration { get; set; }

    [Required(ErrorMessage = "ArtistId is required")]
    public Guid ArtistId { get; set; }
    [Required(ErrorMessage = "AlbumId is required")]
    public Guid AlbumId { get; set; }
}