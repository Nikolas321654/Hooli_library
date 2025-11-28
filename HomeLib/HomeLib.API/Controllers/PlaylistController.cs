using System.Security.Claims;
using HomeLib.API.DataTypes.DataResponse;
using HomeLib.API.Model.DataRequest;
using HomeLib.Core.Exceptions;
using HomeLib.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeLib.API.Controllers;

[Authorize]
[ApiController]
[Route("api/user/playlist")]
public class PlaylistController(IPlaylistService playlistService) : ControllerBase
{
    private Guid GetUserId()
    {
        var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return id == null ? throw new NotFoundException("User not found") : Guid.Parse(id);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPlaylist([FromBody] PaginationRequest pagination,
        CancellationToken cancellationToken = default)
    {
        var playlists =
            await playlistService.GetAllPlaylists(pagination.Page, pagination.PageSize, GetUserId(), cancellationToken);

        var playlistsResponse = playlists.Select(p => new PlaylistResponse
        {
            Id = p.Id,
            Name = p.Name,
            CreatedAt = p.CreatedAt,
        }).ToList();

        return Ok(playlistsResponse);
    }

    [HttpGet("{playlistId:guid}")]
    public async Task<IActionResult> GetPlaylistTracksById(Guid playlistId, CancellationToken cancellationToken)
    {
        var playlist = await playlistService.GetPlaylistById(GetUserId(), playlistId, cancellationToken);

        var playlistResponse = new PlaylistWithTracksResponse
        {
            Id = playlist.Id,
            Name = playlist.Name,
            CreatedAt = playlist.CreatedAt,

            Tracks = playlist.PlaylistTracks.Select(tracks => new PlaylistTrackResponse()
            {
                TrackName = tracks.Track.Name,
                CreatedAt = tracks.CreatedAt,
            }).ToList()
        };

        return Ok(playlistResponse);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePlaylist(CancellationToken cancellationToken,
        [FromBody] PlaylistRequest playlistRequest)
    {
        var newPlaylist = await playlistService.AddPlaylist(GetUserId(), playlistRequest.Name, cancellationToken);
        return Created($"api/user/playlist/{newPlaylist.Id}", newPlaylist);
    }

    [HttpDelete("{playlistId:Guid}")]
    public async Task<IActionResult> DeletePlaylistById(Guid playlistId, CancellationToken cancellationToken)
    {
        await playlistService.HardDeletePlaylist(GetUserId(), playlistId, cancellationToken);
        return NoContent();
    }

    [HttpPut("{playlistId:guid}")]
    public async Task<IActionResult> UpdatePlaylistById(Guid playlistId, CancellationToken cancellationToken,
        [FromBody] PlaylistRequest playlistRequest)
    {
        await playlistService.UpdatePlaylist(GetUserId(), playlistId, playlistRequest.Name, cancellationToken);
        return NoContent();
    }

    [HttpPost("{playlistId:Guid}/tracks")]
    public async Task<IActionResult> AddTrack(Guid playlistId, CancellationToken cancellationToken,
        [FromBody] TrackToPlayListRequest trackToPlayListRequest)
    {
        await playlistService.AddTrack(GetUserId(), playlistId, trackToPlayListRequest.TrackId, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{playlistId:guid}/tracks")]
    public async Task<IActionResult> DeleteTrack(Guid playlistId,
        CancellationToken cancellationToken,
        [FromBody] TrackToPlayListRequest trackToPlayListRequest)
    {
        await playlistService.DeleteTrack(GetUserId(), playlistId, trackToPlayListRequest.TrackId, cancellationToken);
        return NoContent();
    }
}