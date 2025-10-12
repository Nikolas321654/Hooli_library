using HomeLib.API.DataTypes.DataRequest;
using HomeLib.API.DataTypes.DataResponse;
using HomeLib.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HomeLib.API.Artist;

[ApiController]
[Route("api/artist")]
public class ArtistController(IArtistsService artistService, ILogger<ArtistController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllArtists()
    {
        logger.LogInformation("Getting all artists");
        return Ok(await artistService.GetAllArtists());
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetArtistById(Guid id)
    {
        try
        {
            var artist = await artistService.GetArtistById(id);
            logger.LogInformation("Getting artist with id {Guid}", id);
            return artist == null ? throw (new Exception("Artist not found")) : Ok(artist);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting artist with id {Guid}", id);
            return StatusCode(404, ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<ArtistResponce>> AddArtist([FromBody] ArtistRequest artistRequest)
    {
        try
        {
            await artistService.AddArtist(artistRequest.Name, artistRequest.Grammy);

            logger.LogInformation("Added artist with id {string}", artistRequest.Name);
            return Ok("Artist added");
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, "Error adding artist with id {string}", artistRequest.Name);
            return BadRequest("Bad artist name");
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteArtist(Guid id)
    {
        try
        {
            var artist = await artistService.GetArtistById(id);
            if (artist == null) throw new Exception("Artist not found");
            await artistService.DeleteArtist(artist);
            logger.LogInformation("Deleted artist with id {Guid}", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, "Error deleting artist with id {Guid}", id);
            return NoContent();
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ArtistResponce>> UpdateArtist(Guid id, [FromBody] ArtistRequest artistRequest)
    {
        try
        {
            var artist = await artistService.GetArtistById(id);
            if (artist == null) throw new Exception("Artist not found");
                
            artist.Name = artistRequest.Name;
            artist.Grammy = artistRequest.Grammy;
            await artistService.UpdateArtist(artist);

            return Ok(artist);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, $"Error updating artist with id {typeof(Guid)}", id);
            return BadRequest(ex.Message);
        }
    }
}