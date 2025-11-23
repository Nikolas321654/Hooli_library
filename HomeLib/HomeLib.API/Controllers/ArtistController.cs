using HomeLib.API.DataTypes.DataRequest;
using HomeLib.API.DataTypes.DataResponse;
using HomeLib.API.Model.DataResponse;
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
    public async Task<IActionResult> GetAllArtists(CancellationToken cancellationToken)
    {
        var artists = await artistService.GetAllArtists(cancellationToken);
        var artistsResponse = artists.Select(artist => new ArtistResponse()
        {
            ArtistId = artist.Id,
            Name = artist.Name,
            Grammy = artist.Grammy,
        });

        return Ok(artistsResponse);
    }

    [HttpGet("{id:guid}/albums_and_tracks")]
    public async Task<IActionResult> GetArtistWithAlbumsTracksById(Guid id, CancellationToken cancellationToken)
    {
        var artist = await artistService.GetArtistById(id, cancellationToken);
        var artistResponse = new ArtistAlbumsTracksResponse
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

            Tracks = artist.TrackArtists.Select(track => new TrackForArtistResponse()
            {
                TrackId = track.TrackId,
                Name = track.Track.Name,
                Duration = track.Track.Duration
            }).ToList()
        };

        return Ok(artistResponse);
    }

    [HttpGet("{id:guid}/albums")]
    public async Task<IActionResult> GetArtistAlbums(Guid id, CancellationToken cancellationToken)
    {
        var artist = await artistService.GetArtistById(id, cancellationToken);
        var artistResponse = new ArtistAlbumsResponse
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
    public async Task<IActionResult> GetArtistTracks(Guid id, CancellationToken cancellationToken)
    {
        var artist = await artistService.GetArtistById(id, cancellationToken);
        var artistResponse = new ArtistTracksResponse()
        {
            ArtistId = artist.Id,
            Name = artist.Name,
            Grammy = artist.Grammy,
            Tracks = artist.TrackArtists.Select(track => new TrackForArtistResponse()
            {
                TrackId = track.TrackId,
                Name = track.Track.Name,
                Duration = track.Track.Duration
            }).ToList()
        };

        return Ok(artistResponse);
    }

    [HttpPost]
    public async Task<ActionResult> AddArtist(CancellationToken cancellationToken,
        [FromBody] ArtistRequest artistRequest)
    {
        await artistService.AddArtist(artistRequest.Name, artistRequest.Grammy, cancellationToken);
        return Created("api/artist", artistRequest);
    }

    // Carefully    
    [HttpDelete("{id:guid}/hard_delete")]
    public async Task<ActionResult> HardDeleteArtist(Guid id, CancellationToken cancellationToken)
    {
        await artistService.HardDeleteArtist(id, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> SoftDeleteArtist(Guid id, CancellationToken cancellationToken)
    {
        await artistService.SoftDeleteArtist(id, cancellationToken);
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateArtist(Guid id, CancellationToken cancellationToken,
        [FromBody] ArtistRequest artistRequest)
    {
        await artistService.UpdateArtist(artistRequest.Name, artistRequest.Grammy, id, cancellationToken);
        return NoContent();
    }
}