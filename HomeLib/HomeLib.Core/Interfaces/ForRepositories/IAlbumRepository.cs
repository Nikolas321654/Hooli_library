using HomeLib.Core.Model;

namespace HomeLib.Core.Interfaces.ForRepositories;

public interface IAlbumRepository
{
    public Task<List<Album>> GetAllAlbumsAsync(CancellationToken cancellationToken);
    public Task<Album?> GetAlbumByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task AddAlbumAsync(Album album, CancellationToken cancellationToken);
    public Task HardDeleteAlbumAsync(Guid id, CancellationToken cancellationToken);
    public Task UpdateAlbumAsync(Album album, CancellationToken cancellationToken);
    public Task<List<Album>> GetAllDeletedAlbumsAsync(CancellationToken cancellationToken);
    public Task<List<Album>> GetNewAlbums(int count, CancellationToken cancellationToken);
}