using System.ComponentModel.DataAnnotations;

namespace HomeLib.API.Model.DataRequest;

public class TrackPlaylistRequest
{
    [Required(ErrorMessage = "Id is required")]
    public Guid PlaylistId {get; set;}
}