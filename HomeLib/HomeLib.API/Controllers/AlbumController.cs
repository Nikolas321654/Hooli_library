using HomeLib.API.DataTypes.DataResponse;
using HomeLib.API.Model.DataRequest;
using HomeLib.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeLib.API.Album;

[Authorize]
[ApiController]
[Route("api/album")]
public class AlbumController(IAlbumService albumService) : ControllerBase
{
    [HttpGet("tracks")]
    public async Task<IActionResult> GetAllAlbumsWithTracks()
    {
        var albumsList = await albumService.GetAllAlbums();
        var albumsResponse = albumsList.Select(album => new AlbumResponse
        {
            AlbumId = album.Id,
            Name = album.Name,
            Year = album.Year,
            Artist = album.Artist?.Name,
            Tracks = album.Tracks.Select(track => new TrackForAlbumResponse()
            {
                Name = track.Name,
                Duration = track.Duration,
            }).ToList()
        });

        return Ok(albumsResponse);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAlbums()
    {
        var albumsList = await albumService.GetAllAlbums();
        var albumsResponse = albumsList.Select(album => new AlbumResponse
        {
            AlbumId = album.Id,
            Name = album.Name,
            Year = album.Year,
            Artist = album.Artist?.Name,
        });

        return Ok(albumsResponse);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAlbumById(Guid id)
    {
        var album = await albumService.GetAlbumById(id);
        var albumResponse = new AlbumResponse()
        {
            AlbumId = album.Id,
            Name = album.Name,
            Year = album.Year,
            Artist = album.Artist?.Name,
            Tracks = album.Tracks.Select(track => new TrackForAlbumResponse()
            {
                Name = track.Name,
                Duration = track.Duration,
            }).ToList()
        };

        return Ok(albumResponse);
    }

    [HttpPost]
    public async Task<ActionResult> AddAlbum([FromBody] AlbumRequest albumRequest)
    {
        await albumService.AddAlbum(albumRequest.Name, albumRequest.Year, albumRequest.ArtistId);
        return Created("api/album", albumRequest);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAlbum(Guid id, [FromBody] AlbumRequest albumRequest)
    {
        await albumService.UpdateAlbum(id, albumRequest.Name, albumRequest.Year);
        return NoContent();
    }

    // Carefully    
    [HttpDelete("{id:guid}/hard_delete")]
    public async Task<IActionResult> HardDeleteAlbum(Guid id)
    {
        await albumService.HardDeleteAlbum(id);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> SoftDeleteAlbum(Guid id)
    {
        await albumService.SoftDeleteAlbum(id);
        return NoContent();
    }

    [HttpGet("new_albums/{count:int}")]
    public async Task<IActionResult> GetNewAlbums(int count)
    {
        var albums = await albumService.GetNewAlbums(count);
        var albumsResponse = albums.Select(album => new AlbumResponse
        {
            AlbumId = album.Id,
            Name = album.Name,
            Year = album.Year,
            Artist = album.Artist?.Name,
        });

        return Ok(albumsResponse);
    }
}