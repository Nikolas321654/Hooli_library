using HomeLib.Core.Exceptions;
using HomeLib.Core.Interfaces;
using HomeLib.Core.Interfaces.ForRepositories;
using HomeLib.Core.Model;

namespace HomeLib.Services.Services;

public class ArtistService(IArtistRepository artistRepository) : IArtistsService
{
    private async Task<Artist> CheackArtistExist(Guid id, CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty) throw new BadRequestException("Id cannot be empty");
        var artist = await artistRepository.GetArtistByIdAsync(id, cancellationToken);
        return artist ?? throw new NotFoundException($"Artist with {id} id not found");
    }

    public async Task<List<Artist>> GetAllArtists(CancellationToken cancellationToken = default)
    {
        return await artistRepository.GetAllArtistAsync(cancellationToken);
    }

    public async Task<Artist?> GetArtistById(Guid id, CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty) throw new BadRequestException("Id cannot be empty");

        return await artistRepository.GetArtistByIdAsync(id, cancellationToken) ??
               throw new NotFoundException($"Artist with {id} id not found");
    }

    public async Task AddArtist(string name, bool grammy, CancellationToken cancellationToken = default)
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

        await artistRepository.AddArtistAsync(artist, cancellationToken);
    }

    public async Task HardDeleteArtist(Guid id, CancellationToken cancellationToken = default)
    {
        await CheackArtistExist(id, cancellationToken);
        await artistRepository.HardDeleteArtistAsync(id, cancellationToken);
    }

    public async Task UpdateArtist(string name, bool grammy, Guid id, CancellationToken cancellationToken = default)
    {
        var artist = await CheackArtistExist(id, cancellationToken);

        artist.Name = name;
        artist.Grammy = grammy;
        artist.UpdatedAt = DateTime.UtcNow;

        await artistRepository.UpdateArtistAsync(artist, cancellationToken);
    }

    public async Task SoftDeleteArtist(Guid id, CancellationToken cancellationToken = default)
    {
        var artist = await CheackArtistExist(id, cancellationToken);
        artist.IsDeleted = true;
        artist.UpdatedAt = DateTime.UtcNow;

        await artistRepository.UpdateArtistAsync(artist, cancellationToken);
    }
}