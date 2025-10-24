using HomeLib.API.DataTypes.DataRequest;
using HomeLib.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeLib.API.Artist;
[Authorize]
[ApiController]
[Route("api/artist")]
public class ArtistController(IArtistsService artistService, ILogger<ArtistController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllArtists()
    {
        return Ok(await artistService.GetAllArtists());
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetArtistById(Guid id)
    {
        return Ok(await artistService.GetArtistById(id));
    }

    [HttpPost]
    public async Task<ActionResult> AddArtist([FromBody] ArtistRequest artistRequest)
    {
        await artistService.AddArtist(artistRequest.Name, artistRequest.Grammy);
        return Created();
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