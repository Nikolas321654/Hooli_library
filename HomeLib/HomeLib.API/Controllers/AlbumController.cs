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
    public async Task<IActionResult> GetAllAlbumsWithTracks([FromBody] PaginationRequest pagination,
        CancellationToken cancellationToken = default
    )
    {
        var albumsList = await albumService.GetAllAlbums(pagination.Page, pagination.PageSize, cancellationToken);
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
    public async Task<IActionResult> GetAllAlbums([FromBody] PaginationRequest pagination,
        CancellationToken cancellationToken = default)
    {
        var albumsList = await albumService.GetAllAlbums(pagination.Page, pagination.PageSize, cancellationToken);
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
    public async Task<IActionResult> GetAlbumById(Guid id, CancellationToken cancellationToken)
    {
        var album = await albumService.GetAlbumById(id, cancellationToken);
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
    public async Task<ActionResult> AddAlbum([FromBody] AlbumRequest albumRequest, CancellationToken cancellationToken)
    {
        await albumService.AddAlbum(albumRequest.Name, albumRequest.Year, albumRequest.ArtistId, cancellationToken);
        return Created($"api/album", albumRequest);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAlbum(Guid id, [FromBody] AlbumRequest albumRequest,
        CancellationToken cancellationToken)
    {
        await albumService.UpdateAlbum(id, albumRequest.Name, albumRequest.Year, cancellationToken);
        return NoContent();
    }

    // Carefully    
    [HttpDelete("{id:guid}/hard_delete")]
    public async Task<IActionResult> HardDeleteAlbum(Guid id, CancellationToken cancellationToken)
    {
        await albumService.HardDeleteAlbum(id, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> SoftDeleteAlbum(Guid id, CancellationToken cancellationToken)
    {
        await albumService.SoftDeleteAlbum(id, cancellationToken);
        return NoContent();
    }

    [HttpGet("new_albums/{count:int}")]
    public async Task<IActionResult> GetNewAlbums(int count, CancellationToken cancellationToken)
    {
        var albums = await albumService.GetNewAlbums(count, cancellationToken);
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