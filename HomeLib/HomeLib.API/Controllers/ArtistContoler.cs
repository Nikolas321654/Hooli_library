using HomeLib.API.DataTypes.DataRequest;
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

    [HttpPost]
    public async Task<ActionResult<ArtistRequest>> AddArtist([FromBody] ArtistRequest artistRequest)
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
        return Created($"artist/{artist.Id}", artist);
    }
}