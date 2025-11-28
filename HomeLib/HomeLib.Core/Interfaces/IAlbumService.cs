using HomeLib.Core.Model;

namespace HomeLib.Core.Interfaces;

public interface IAlbumService
{
    public Task<Album> GetAlbumById(Guid albumId, CancellationToken cancellationToken);
    public Task<List<Album>> GetAllAlbums(int page, int pageSize, CancellationToken cancellationToken);
    public Task HardDeleteAlbum(Guid albumId, CancellationToken cancellationToken);
    public Task SoftDeleteAlbum(Guid albumId, CancellationToken cancellationToken);
    public Task<Guid> AddAlbum(string name, int year, Guid artistId, CancellationToken cancellationToken);
    public Task UpdateAlbum(Guid albumId, string name, int year, CancellationToken cancellationToken);
    public Task<List<Album>> GetNewAlbums(int count, CancellationToken cancellationToken);
}