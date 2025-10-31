using HomeLib.API.DataTypes.DataRequest;
using HomeLib.API.DataTypes.DataResponse;
using HomeLib.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeLib.API.Track;

[Authorize]
[ApiController]
[Route("api/track")]
public class TrackController(ITrackService trackService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllTracks()
    {
        var tracks = await trackService.GetAllTracks();
        var tracksResponse = tracks.Select(track => new TrackResponse()
        {
            Name = track.Name,
            Artist = track.Artist?.Name,
            Album = track.Album?.Name,
            Duration = track.Duration,
            TrackId = track.Id,
        }).ToList();

        return Ok(tracksResponse);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetTrackById(Guid id)
    {
        var track = await trackService.GetTrackById(id);
        var trackResponse = new TrackResponse()
        {
            Name = track.Name,
            Artist = track.Artist?.Name,
            Album = track.Album?.Name,
            Duration = track.Duration,
            TrackId = track.Id,
        };

        return Ok(trackResponse);
    }

    [HttpPost]
    public async Task<ActionResult> AddTrack([FromBody] TrackRequest trackRequest)
    {
        await trackService.AddTrack(
            trackRequest.Name,
            trackRequest.AlbumId,
            trackRequest.ArtistId,
            trackRequest.Duration);

        return Created("api/track", trackRequest);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateTrack(Guid id, [FromBody] TrackUpdateRequest trackUpdate)
    {
        await trackService.UpdateTrack(id, trackUpdate.Name, trackUpdate.Duration);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTrack(Guid id)
    {
        await trackService.DeleteTrack(id);
        return NoContent();
    }
}