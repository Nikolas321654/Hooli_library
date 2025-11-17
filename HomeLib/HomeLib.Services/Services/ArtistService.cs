using HomeLib.Core;
using HomeLib.Core.Exceptions;
using HomeLib.Core.Interfaces;
using HomeLib.Core.Interfaces.ForRepositories;
using HomeLib.Core.Model;

namespace HomeLib.Services.Services;

public class ArtistService(IArtistRepository artistRepository) : IArtistsService
{
    public async Task<List<Artist>> GetAllArtists()
    {
        return await artistRepository.GetAllArtistAsync();
    }

    public async Task<Artist?> GetArtistById(Guid id)
    {
        if (id == Guid.Empty) throw new BadRequestException("Id cannot be empty");

        return await artistRepository.GetArtistByIdAsync(id) ??
               throw new NotFoundException($"Artist with {id} id not found");
    }

    public async Task AddArtist(string name, bool grammy)
    {
        var artist = new Artist()
        {
            Id = Guid.NewGuid(),
            Name = name,
            Grammy = grammy,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsDeleted = false
        };

        await artistRepository.AddArtistAsync(artist);
    }

    public async Task HardDeleteArtist(Guid id)
    {
        if (id == Guid.Empty) throw new BadRequestException("Id cannot be empty");
        var artist = await artistRepository.GetArtistByIdAsync(id);
        if (artist == null) throw new NotFoundException($"Artist with {id} id not found");
        await artistRepository.HardDeleteArtistAsync(id);
    }

    public async Task UpdateArtist(string name, bool grammy, Guid id)
    {
        if (id == Guid.Empty) throw new BadRequestException("Id cannot be empty");
        var artist = await artistRepository.GetArtistByIdAsync(id);
        if (artist == null) throw new NotFoundException($"Artist with {id} id not found");

        artist.Name = name;
        artist.Grammy = grammy;
        artist.UpdatedAt = DateTime.UtcNow;

        await artistRepository.UpdateArtistAsync(artist);
    }

    public async Task SoftDeleteArtist(Guid id)
    {
        if (id == Guid.Empty) throw new BadRequestException("Id cannot be empty");
        var artist = await artistRepository.GetArtistByIdAsync(id);
        if (artist == null) throw new NotFoundException($"Artist with {id} id not found");

        artist.IsDeleted = true;
        artist.UpdatedAt = DateTime.UtcNow;

        await artistRepository.UpdateArtistAsync(artist);
    }
}