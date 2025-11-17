using System.ComponentModel.DataAnnotations;

namespace HomeLib.API.DataTypes.DataResponse;

public class TrackUpdateRequest
{
    [MinLength(3, ErrorMessage = "Name is too short")]
    [MaxLength(20, ErrorMessage = "Name is too long")]
    public string Name { get; set; }

    [Range(1, 3600, ErrorMessage = "Duration must be between 1 and 3600")]
    public int Duration { get; set; }
}