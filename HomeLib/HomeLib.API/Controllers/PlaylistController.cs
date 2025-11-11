using System.Security.Claims;
using HomeLib.API.DataTypes.DataResponse;
using HomeLib.API.Model.DataRequest;
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
        return id == null ? throw new Exception("User not found") : Guid.Parse(id);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPlaylist()
    {
        var playlists = await playlistService.GetAllPlaylists(GetUserId());
        var playlistsResponse = playlists.Select(playlists => new PlaylistResponse
        {
            Id = playlists.Id,
            Name = playlists.Name,
            CreatedAt = playlists.CreatedAt,
        }).ToList();

        return Ok(playlistsResponse);
    }

    [HttpGet("{playlistId:guid}")]
    public async Task<IActionResult> GetPlaylistTracksById(Guid playlistId)
    {
        var playlist = await playlistService.GetPlaylistById(GetUserId(), playlistId);

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
    public async Task<IActionResult> CreatePlaylist([FromBody] PlaylistRequest playlistRequest)
    {
        var newPlaylist = await playlistService.AddPlaylist(GetUserId(), playlistRequest.Name);
        return CreatedAtAction(nameof(GetPlaylistTracksById), new { playlistId = newPlaylist.Id }, newPlaylist);
    }

    [HttpDelete("{playlistId:Guid}")]
    public async Task<IActionResult> DeletePlaylistById(Guid playlistId)
    {
        await playlistService.DeletePlaylist(GetUserId(), playlistId);
        return NoContent();
    }

    [HttpPut("{playlistId:guid}")]
    public async Task<IActionResult> UpdatePlaylistById(Guid playlistId, [FromBody] PlaylistRequest playlistRequest)
    {
        await playlistService.UpdatePlaylist(GetUserId(), playlistId, playlistRequest.Name);
        return NoContent();
    }

    [HttpPost("{playlistId:Guid}/tracks")]
    public async Task<IActionResult> AddTrack(Guid playlistId, [FromBody] TrackToPlayListRequest trackToPlayListRequest)
    {
        await playlistService.AddTrack(GetUserId(), playlistId, trackToPlayListRequest.TrackId);
        return NoContent();
    }

    [HttpDelete("{playlistId:guid}/tracks")]
    public async Task<IActionResult> DeleteTrack(Guid playlistId,
        [FromBody] TrackToPlayListRequest trackToPlayListRequest)
    {
        await playlistService.DeleteTrack(GetUserId(), playlistId, trackToPlayListRequest.TrackId);
        return NoContent();
    }
}