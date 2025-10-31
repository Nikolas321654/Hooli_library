using HomeLib.API.DataTypes.DataRequest;
using HomeLib.API.DataTypes.DataResponse;
using HomeLib.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeLib.API.Artist;

[Authorize]
[ApiController]
[Route("api/artist")]
public class ArtistController(IArtistsService artistService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllArtists()
    {
        var artists = await artistService.GetAllArtists();
        var artistsResponse = artists.Select(artist => new ArtistResponse
        {
            ArtistId = artist.Id,
            Name = artist.Name,
            Grammy = artist.Grammy,
            Albums = artist.Albums.Select(album => new AlbumForArtistResponse()
            {
                Name = album.Name,
                Year = album.Year,
                AlbumId = album.Id,
            }).ToList(),

            Tracks = artist.Tracks.Select(track => new TrackForArtistResponse()
            {
                Name = track.Name,
                Duration = track.Duration,
                TrackId = track.Id,
            }).ToList()
        });

        return Ok(artistsResponse);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetArtistById(Guid id)
    {
        var artist = await artistService.GetArtistById(id);
        var artistResponse = new ArtistResponse
        {
            ArtistId = artist.Id,
            Name = artist.Name,
            Grammy = artist.Grammy,
            Albums = artist.Albums.Select(album => new AlbumForArtistResponse()
            {
                Name = album.Name,
                Year = album.Year,
                AlbumId = album.Id,
            }).ToList(),

            Tracks = artist.Tracks.Select(track => new TrackForArtistResponse()
            {
                Name = track.Name,
                Duration = track.Duration,
                TrackId = track.Id,
            }).ToList()
        };

        return Ok(artistResponse);
    }

    [HttpGet("{id:guid}/albums")]
    public async Task<IActionResult> GetArtistAlbums(Guid id)
    {
        var artists = await artistService.GetArtistById(id);
        var artist = await artistService.GetArtistById(id);
        var artistResponse = new ArtistResponse
        {
            ArtistId = artist.Id,
            Name = artist.Name,
            Grammy = artist.Grammy,
            Albums = artist.Albums.Select(album => new AlbumForArtistResponse()
            {
                Name = album.Name,
                Year = album.Year,
                AlbumId = album.Id,
            }).ToList(),
        };

        return Ok(artistResponse);
    }

    [HttpGet("{id:guid}/tracks")]
    public async Task<IActionResult> GetArtistTracks(Guid id)
    {
        var artists = await artistService.GetArtistById(id);
        var artist = await artistService.GetArtistById(id);
        var artistResponse = new ArtistResponse
        {
            ArtistId = artist.Id,
            Name = artist.Name,
            Grammy = artist.Grammy,
            Tracks = artist.Tracks.Select(track => new TrackForArtistResponse()
            {
                Name = track.Name,
                Duration = track.Duration,
                TrackId = track.Id,
            }).ToList()
        };

        return Ok(artistResponse);
    }

    [HttpPost]
    public async Task<ActionResult> AddArtist([FromBody] ArtistRequest artistRequest)
    {
        await artistService.AddArtist(artistRequest.Name, artistRequest.Grammy);
        return Created("api/artist", artistRequest);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteArtist(Guid id)
    {
        await artistService.DeleteArtist(id);
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateArtist(Guid id, [FromBody] ArtistRequest artistRequest)
    {
        await artistService.UpdateArtist(artistRequest.Name, artistRequest.Grammy, id);
        return NoContent();
    }
}