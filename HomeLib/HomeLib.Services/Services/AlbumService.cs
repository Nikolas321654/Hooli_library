using HomeLib.Core.Exceptions;
using HomeLib.Core.Interfaces;
using HomeLib.Core.Interfaces.ForRepositories;
using HomeLib.Core.Model;

namespace HomeLib.Services.Services;

public class AlbumService(IAlbumRepository albumRepository) : IAlbumService
{
    private async Task<Album> CheackAlbumExist(Guid id, CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty) throw new BadRequestException("Id cannot be empty");
        var album = await albumRepository.GetAlbumByIdAsync(id, cancellationToken);
        return album ?? throw new NotFoundException($"Album with {id} id not found");
    }

    public async Task<Album> GetAlbumById(Guid id, CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty) throw new BadRequestException("Id cannot be empty");
        var album = await albumRepository.GetAlbumByIdAsync(id, cancellationToken);
        return album ?? throw new NotFoundException($"Album with {id} id not found");
    }

    public async Task<List<Album>> GetAllAlbums(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 1;
        if (pageSize > 100) pageSize = 100;
        return await albumRepository.GetAllAlbumsAsync(page, pageSize, cancellationToken);
    }

    public async Task HardDeleteAlbum(Guid id, CancellationToken cancellationToken = default)
    {
        await CheackAlbumExist(id, cancellationToken);
        await albumRepository.HardDeleteAlbumAsync(id, cancellationToken);
    }

    public async Task SoftDeleteAlbum(Guid id, CancellationToken cancellationToken = default)
    {
        var album = await CheackAlbumExist(id, cancellationToken);

        album.IsDeleted = true;
        album.UpdatedAt = DateTime.UtcNow;

        await albumRepository.UpdateAlbumAsync(album, cancellationToken);
    }


    public async Task<Guid> AddAlbum(string name, int year, Guid artistId,
        CancellationToken cancellationToken = default)
    {
        var newId = Guid.NewGuid();
        var album = new Album()
        {
            Id = newId,
            Name = name,
            Year = year,
            ArtistId = artistId,
            IsDeleted = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await albumRepository.AddAlbumAsync(album, cancellationToken);
        return newId;
    }

    public async Task UpdateAlbum(Guid id, string name, int year, CancellationToken cancellationToken = default)
    {
        var album = await CheackAlbumExist(id, cancellationToken);

        album.Name = name;
        album.Year = year;
        album.UpdatedAt = DateTime.UtcNow;
        await albumRepository.UpdateAlbumAsync(album, cancellationToken);
    }

    public async Task<List<Album>> GetNewAlbums(int count, CancellationToken cancellationToken = default)
    {
        return await albumRepository.GetNewAlbums(count, cancellationToken);
    }
}