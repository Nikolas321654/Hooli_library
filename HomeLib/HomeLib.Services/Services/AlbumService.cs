using HomeLib.Core;
using HomeLib.Core.Exceptions;
using HomeLib.Core.Interfaces;
using HomeLib.Core.Interfaces.ForRepositories;
using HomeLib.Core.Model;

namespace HomeLib.Services.Services;

public class AlbumService(IAlbumRepository albumRepository) : IAlbumService
{
    public async Task<Album> GetAlbumById(Guid id)
    {
        if(id == Guid.Empty) throw new BadRequestException("Id cannot be empty");
        var album = await albumRepository.GetAlbumByIdAsync(id);
        return album ?? throw new NotFoundException($"Album with {id} id not found");
    }

    public async Task<List<Album>> GetAllAlbums()
    {
        return await albumRepository.GetAllAlbumsAsync();
    }

    public async Task DeleteAlbum(Guid id)
    {
        if(id == Guid.Empty) throw new BadRequestException("Id cannot be empty");
        var album = await albumRepository.GetAlbumByIdAsync(id);
        if (album == null) throw new NotFoundException($"Album with {id} id not found");
        await albumRepository.DeleteAlbumAsync(id);
    }

    public async Task AddAlbum(string name, int year, Guid artistId)
    {
        var album = new Album()
        {
            Id = Guid.NewGuid(),
            Name = name,
            Year = year,
            ArtistId = artistId
        };

        await albumRepository.AddAlbumAsync(album);
    }

    public async Task UpdateAlbum(Guid id, string name, int year)
    {
        if(id == Guid.Empty) throw new BadRequestException("Id cannot be empty");
        var album = await albumRepository.GetAlbumByIdAsync(id);
        if (album == null) throw new NotFoundException($"Album with {id} id not found");
        
        album.Name = name;
        album.Year = year;
        await albumRepository.UpdateAlbumAsync(album);
    }
}