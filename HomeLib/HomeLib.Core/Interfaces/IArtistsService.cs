using HomeLib.Core.Model;

namespace HomeLib.Core.Interfaces;

public interface IArtistsService
{
    public Task<List<Artist>> GetAllArtists(int page, int pageSize, CancellationToken cancellationToken);
    public Task<Artist?> GetArtistById(Guid id, CancellationToken cancellationToken);
    public Task UpdateArtist(string name, bool grammy, Guid id, CancellationToken cancellationToken);
    public Task AddArtist(string name, bool grammy, CancellationToken cancellationToken);
    public Task HardDeleteArtist(Guid id, CancellationToken cancellationToken);
    public Task SoftDeleteArtist(Guid id, CancellationToken cancellationToken);
}