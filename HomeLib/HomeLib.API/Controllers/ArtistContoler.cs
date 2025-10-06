using HomeLib.API.DataTypes.DataRequest;
using HomeLib.API.DataTypes.DataResponse;
using HomeLib.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HomeLib.API.Artist;

[ApiController]
[Route("api/artist")]
public class ArtistController(IArtistsService artistService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllArtists()
    {
        return Ok(await artistService.GetAllArtists());
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetArtistById(Guid id)
    {
        var artist = await artistService.GetArtistById(id);

        if (artist == null)
            return NotFound("Artist not found");

        return Ok(artist);
    }

    [HttpPost]
    public async Task<ActionResult<ArtistResponce>> AddArtist([FromBody] ArtistRequest artistRequest)
    {
        var artist = new HomeLib.Core.Artist
        {
            Name = artistRequest.Name,
            Grammy = artistRequest.Grammy
        };

        if (string.IsNullOrEmpty(artist.Name) || artist.Name.Length < 1)
        {
            return BadRequest("Bad artist name");
        }

        await artistService.AddArtist(artist);
        return CreatedAtAction(nameof(GetArtistById), new { id = artist.Id }, artist);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteArtist(Guid id)
    {
        var artist = await artistService.GetArtistById(id);

        if (artist == null)
            return NotFound("Artist not found");

        await artistService.DeleteArtist(artist);
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ArtistResponce>> UpdateArtist(Guid id, [FromBody] ArtistRequest artistRequest)
    {
        var artist = await artistService.GetArtistById(id);
        if (artist == null)
            return NotFound("Artist not found");

        artist.Name = artistRequest.Name;
        artist.Grammy = artistRequest.Grammy;
        await artistService.UpdateArtist(artist);

        return Ok(artist);
    }
}