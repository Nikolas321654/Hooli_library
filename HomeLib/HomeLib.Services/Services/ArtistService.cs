using HomeLib.Core;
using HomeLib.Core.Interfaces;
using HomeLib.Core.Interfaces.ForRepositories;

namespace HomeLib.Services.Services;

public class ArtistService(IArtistRepository artistRepository) : IArtistsService
{
    public async Task<List<Artist>> GetAllArtists()
    {
        return await artistRepository.GetAllArtistAsync();
    }

    public async Task AddArtist(Artist artist)
    {
        await artistRepository.AddArtistAsync(artist);
    }
}