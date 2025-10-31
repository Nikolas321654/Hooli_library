using HomeLib.Core.Model;

namespace HomeLib.Core.Interfaces.ForRepositories;

public interface IAlbumRepository
{
    public Task<List<Album>> GetAllAlbumsAsync();
    public Task<Album?> GetAlbumByIdAsync(Guid id);
    public Task AddAlbumAsync(Album album);
    public Task DeleteAlbumAsync(Guid id);
    public Task UpdateAlbumAsync(Album album);
}