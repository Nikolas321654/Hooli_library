using HomeLib.API.DataTypes.DataRequest;
using HomeLib.API.DataTypes.DataResponse;
using HomeLib.API.Model.DataRequest;
using HomeLib.API.Model.DataResponse;
using HomeLib.Core.Application.Artists.Commands;
using HomeLib.Core.Application.Artists.Handlers;
using HomeLib.Core.Application.Artists.Queries;
using HomeLib.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeLib.API.Artist;

[Authorize]
[ApiController]
[Route("api/artist")]
public class ArtistController(
    GetAllArtistsQueryHandler getAllArtistsQueryHandler,
    GetArtistByIdQueryHandler getArtistByIdQueryHandler,
    GetArtistAlbumsQueryHandler getArtistAlbumsQueryHandler,
    GetArtistTracksQueryHandler getArtistTracksQueryHandler,
    GetArtistWithAlbumsTracksQueryHandler getArtistWithAlbumsTracksQueryHandler,
    AddArtistCommandHandler addArtistCommandHandler,
    UpdateArtistCommandHandler updateArtistCommandHandler,
    SoftDeleteArtistCommandHandler softDeleteArtistCommandHandler,
    HardDeleteArtistCommandHandler hardDeleteArtistCommandHandler
) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllArtists([FromBody] PaginationRequest pagination,
        CancellationToken cancellationToken = default)
    {
        var query = new GetAllArtistsQuery(pagination.Page, pagination.PageSize);
        var artists = await getAllArtistsQueryHandler.Handle(query, cancellationToken);
        
        var artistsResponse = artists.Select(artist => new ArtistResponse()
        {
            ArtistId = artist.ArtistId,
            Name = artist.Name,
            Grammy = artist.Grammy,
        });

        return Ok(artistsResponse);
    }

    [HttpGet("{id:guid}/albums_and_tracks")]
    public async Task<IActionResult> GetArtistWithAlbumsTracksById(Guid id, PaginationRequest pagination, CancellationToken cancellationToken = default)
    {
        var query = new GetArtistWithAlbumsTracksQuery(id, pagination.Page, pagination.PageSize);
        var artist = await getArtistWithAlbumsTracksQueryHandler.Handle(query, cancellationToken);
        
        var artistResponse = new ArtistAlbumsTracksResponse
        {
            ArtistId = artist.ArtistId,
            Name = artist.Name,
            Grammy = artist.Grammy,
            Albums = artist.Albums
                .Select(album => new AlbumForArtistResponse()
                {
                    Name = album.Name,
                    Year = album.Year,
                    AlbumId = album.AlbumId,
                }).ToList(),

            Tracks = artist.Tracks
                .Select(track => new TrackForArtistResponse()
                {
                    TrackId = track.TrackId,
                    Name = track.Name,
                    Duration = track.Duration
                }).ToList()
        };

        return Ok(artistResponse);
    }

    [HttpGet("{id:guid}/albums")]
    public async Task<IActionResult> GetArtistAlbums(Guid id, [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20, CancellationToken cancellationToken = default)
    {
        var query = new GetArtistAlbumsQuery(id, page, pageSize);
        var artist = await getArtistAlbumsQueryHandler.Handle(query, cancellationToken);
        
        var artistResponse = new ArtistAlbumsResponse
        {
            ArtistId = artist.ArtistId,
            Name = artist.Name,
            Grammy = artist.Grammy,
            Albums = artist.Albums
                .Select(album => new AlbumForArtistResponse()
                {
                    Name = album.Name,
                    Year = album.Year,
                    AlbumId = album.AlbumId,
                }).ToList(),
        };

        return Ok(artistResponse);
    }

    [HttpGet("{id:guid}/tracks")]
    public async Task<IActionResult> GetArtistTracks(Guid id, [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20, CancellationToken cancellationToken = default)
    {
        var query = new GetArtistTracksQuery(id, page, pageSize);
        var artist = await getArtistTracksQueryHandler.Handle(query, cancellationToken);
        
        var artistResponse = new ArtistTracksResponse()
        {
            ArtistId = artist.ArtistId,
            Name = artist.Name,
            Grammy = artist.Grammy,
            Tracks = artist.Tracks
                .Select(track => new TrackForArtistResponse()
                {
                    TrackId = track.TrackId,
                    Name = track.Name,
                    Duration = track.Duration
                }).ToList()
        };

        return Ok(artistResponse);
    }

    [HttpPost]
    public async Task<ActionResult> AddArtist(CancellationToken cancellationToken,
        [FromBody] ArtistRequest artistRequest)
    {
        var command = new AddArtistCommand(artistRequest.Name, artistRequest.Grammy);
        var id = await addArtistCommandHandler.Handle(command, cancellationToken);
        return Created("api/artist", new { id, artistRequest.Name, artistRequest.Grammy });
    }

    // Carefully    
    [HttpDelete("{id:guid}/hard_delete")]
    public async Task<ActionResult> HardDeleteArtist(Guid id, CancellationToken cancellationToken)
    {
        var command = new HardDeleteArtistCommand(id);
        await hardDeleteArtistCommandHandler.Handle(command, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> SoftDeleteArtist(Guid id, CancellationToken cancellationToken)
    {
        var command = new SoftDeleteArtistCommand(id);
        await softDeleteArtistCommandHandler.Handle(command, cancellationToken);
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateArtist(Guid id, CancellationToken cancellationToken,
        [FromBody] ArtistRequest artistRequest)
    {
        var command = new UpdateArtistCommand(id, artistRequest.Name, artistRequest.Grammy);
        await updateArtistCommandHandler.Handle(command, cancellationToken);
        return NoContent();
    }
}