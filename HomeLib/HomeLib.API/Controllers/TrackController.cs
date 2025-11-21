using HomeLib.API.DataTypes.DataRequest;
using HomeLib.API.DataTypes.DataResponse;
using HomeLib.API.Model.DataRequest.Track;
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
        var tracksResponse = tracks.Select(t => new TrackResponse()
        {
            Name = t.Name,
            ArtistName = t.TrackArtists.Select(ta => ta.Artist?.Name).FirstOrDefault(),
            AlbumName = t.Album?.Name,
            Duration = t.Duration,
            TrackId = t.Id,
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
            ArtistName = track.TrackArtists.Select(t => t.Artist?.Name).FirstOrDefault(),
            AlbumName = track.Album?.Name,
            Duration = track.Duration,
        };

        return Ok(trackResponse);
    }

    [HttpGet("new_track/{count:int}")]
    public async Task<IActionResult> GetNewTracks(int count)
    {
        var tracks = await trackService.GetNewTracks(count);
        var tracksResponse = tracks.Select(t => new TrackResponse()
        {
            Name = t.Name,
            ArtistName = t.TrackArtists.Select(ta => ta.Artist?.Name).FirstOrDefault(),
            AlbumName = t.Album?.Name,
            Duration = t.Duration,
            TrackId = t.Id,
        }).ToList();
        return Ok(tracksResponse);
    }

    [HttpPost]
    public async Task<ActionResult> AddTrack([FromBody] TrackRequest trackRequest)
    {
        await trackService.AddTrack(
            trackRequest.Name,
            trackRequest.AlbumId,
            trackRequest.ArtistId,
            trackRequest.Duration
        );

        return Ok("Created track");
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateTrack(Guid id, [FromBody] TrackUpdateRequest trackUpdate)
    {
        await trackService.UpdateTrack(id, trackUpdate.Name, trackUpdate.Duration);
        return NoContent();
    }

    // Carefully
    [HttpDelete("{id:guid}/hard_delete")]
    public async Task<IActionResult> HardDeleteTrack(Guid id)
    {
        await trackService.HardDeleteTrack(id);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> SoftDeleteTrack(Guid id)
    {
        await trackService.SoftDeleteTrack(id);
        return NoContent();
    }
}