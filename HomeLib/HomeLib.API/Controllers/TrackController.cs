using HomeLib.API.DataTypes.DataRequest;
using HomeLib.API.DataTypes.DataResponse;
using HomeLib.API.Model.DataRequest;
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
    public async Task<IActionResult> GetAllTracks([FromBody] PaginationRequest pagination,
        CancellationToken cancellationToken = default)
    {
        var tracks = await trackService.GetAllTracks(pagination.Page, pagination.PageSize, cancellationToken);
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
    public async Task<IActionResult> GetTrackById(Guid id, CancellationToken cancellationToken)
    {
        var track = await trackService.GetTrackById(id, cancellationToken);
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
    public async Task<IActionResult> GetNewTracks(int count, CancellationToken cancellationToken)
    {
        var tracks = await trackService.GetNewTracks(count, cancellationToken);
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
    public async Task<ActionResult> AddTrack(CancellationToken cancellationToken, [FromBody] TrackRequest trackRequest)
    {
        var track = await trackService.AddTrack(
            trackRequest.Name,
            trackRequest.AlbumId,
            trackRequest.ArtistId,
            trackRequest.Duration,
            cancellationToken
        );

        new TrackResponse()
        {
            TrackId = track.Id,
            Name = track.Name,
            ArtistName = track.TrackArtists.Select(ta => ta.Artist?.Name).FirstOrDefault(),
            AlbumName = track.Album?.Name,
            Duration = track.Duration,
        };
        return Created("api/track", trackRequest);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateTrack(Guid id, CancellationToken cancellationToken,
        [FromBody] TrackUpdateRequest trackUpdate)
    {
        await trackService.UpdateTrack(id, trackUpdate.Name, trackUpdate.Duration, cancellationToken);
        return NoContent();
    }

    // Carefully
    [HttpDelete("{id:guid}/hard_delete")]
    public async Task<IActionResult> HardDeleteTrack(Guid id, CancellationToken cancellationToken)
    {
        await trackService.HardDeleteTrack(id, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> SoftDeleteTrack(Guid id, CancellationToken cancellationToken)
    {
        await trackService.SoftDeleteTrack(id, cancellationToken);
        return NoContent();
    }
}