using HomeLib.Core.Model;

namespace HomeLib.Core.Interfaces;

public interface IAlbumService
{
    public Task<Album> GetAlbumById(Guid albumId);
    public Task<List<Album>> GetAllAlbums();
    public Task HardDeleteAlbum(Guid albumId);
    public Task SoftDeleteAlbum(Guid albumId);
    public Task AddAlbum(string name, int year, Guid artistId);
    public Task UpdateAlbum(Guid albumId, string name, int year);
    public Task<List<Album>> GetNewAlbums(int count);
}