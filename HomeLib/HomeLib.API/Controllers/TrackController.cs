using HomeLib.API.DataTypes.DataRequest;
using HomeLib.Core.Interfaces;
using HomeLib.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeLib.API.Track;

[Authorize]
[ApiController]
[Route("api/track")]
public class TrackController(ITrackService trackService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetTracks()
    {
        return Ok(await trackService.GetAllTracks());
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetTrackById(Guid id)
    {
        return Ok(await trackService.GetTrackById(id));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTrack(Guid id)
    {
        return NoContent();
    }

    [HttpPost("{id:guid}")]
    public async Task<IActionResult> UpdateTrack()
    {
        return Ok();
    }
    
    
    [HttpPost]
    public async Task<ActionResult<TrackRequest>> AddTrack([FromBody] TrackRequest trackRequest)
    {
        // await trackService.AddTrack(trackRequest.AlbumId, trackRequest.ArtistId, trackRequest.Name, );
        // return Created($"track/{coreTrack.Id}", coreTrack.Id);
        return Ok(trackRequest);
    }
}