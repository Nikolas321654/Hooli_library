using HomeLib.API.DataTypes.DataRequest;
using HomeLib.Core.Interfaces;
using HomeLib.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace HomeLib.API.Track;

[ApiController]
[Route("api/[controller]")]
public class TrackController(ITrackService trackService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetTracks()
    {
        return Ok(await trackService.GetAllTracks());
    }

    [HttpPost]
    public async Task<ActionResult<TrackRequest>> AddTrack([FromBody] TrackRequest trackRequest)
    {
        var coreTrack = new HomeLib.Core.Track
        {
            Name = trackRequest.Name,
            ArtistId = trackRequest.ArtistId,
            AlbumId = trackRequest.AlbumId,
            Duration = trackRequest.Duration
        };

        await trackService.AddTrack(coreTrack);
        return Created($"track/{coreTrack.Id}", coreTrack.Id);
    }
}