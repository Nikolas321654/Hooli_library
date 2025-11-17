using System.ComponentModel.DataAnnotations;

namespace HomeLib.API.Model.DataRequest;

public class NewAlbumsRequest
{
    [Required]
    [MinLength(1, ErrorMessage = "Title must be at least 1 character long.")]
    [MaxLength(30, ErrorMessage = "Title must be less than 30 character long.")]
    public int Count { get; set; }
}