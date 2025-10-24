using HomeLib.Core;
using HomeLib.Core.Exceptions;
using HomeLib.Core.Interfaces;
using HomeLib.Core.Interfaces.ForRepositories;

namespace HomeLib.Services.Services;

public class ArtistService(IArtistRepository artistRepository) : IArtistsService
{
    public async Task<List<Artist>> GetAllArtists()
    {
        return await artistRepository.GetAllArtistAsync();
    }

    public async Task<Artist> GetArtistById(Guid id)
    {
        if (id == Guid.Empty) throw new BadRequestException("Id cannot be empty");
        return await artistRepository.GetArtistByIdAsync(id) ?? throw new NotFoundException($"Artist with {id} id not found");
    }

    public async Task AddArtist(string name, bool grammy)
    {
        await artistRepository.AddArtistAsync(name, grammy);
    }

    public async Task DeleteArtist(Guid id)
    {
        if (id == Guid.Empty) throw new BadRequestException("Id cannot be empty");
        if (!await artistRepository.ExistingAsync(id)) throw new NotFoundException($"Artist with {id} id not found");
        await artistRepository.DeleteArtistAsync(id);
    }

    public async Task UpdateArtist(string name, bool grammy, Guid id)
    {
        if (id == Guid.Empty) throw new BadRequestException("Id cannot be empty");
        if(!await artistRepository.ExistingAsync(id)) throw new NotFoundException($"Artist with {id} id not found");
        await artistRepository.UpdateArtistAsync(name, grammy, id);
    }
}