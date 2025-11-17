using System.ComponentModel.DataAnnotations;

namespace HomeLib.API.Model.DataRequest;

public class TrackToPlayListRequest
{
    [Required(ErrorMessage = "TrackId is required")]
    public Guid TrackId { get; set; }
}