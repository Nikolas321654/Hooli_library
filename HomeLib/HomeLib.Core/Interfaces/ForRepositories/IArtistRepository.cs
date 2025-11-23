using HomeLib.Core.Model;

namespace HomeLib.Core.Interfaces.ForRepositories;

public interface IArtistRepository
{
    public Task<List<Artist>> GetAllArtistAsync(CancellationToken cancellationToken = default);
    public Task AddArtistAsync(Artist artist, CancellationToken cancellationToken = default);
    public Task UpdateArtistAsync(Artist artist, CancellationToken cancellationToken = default);
    public Task HardDeleteArtistAsync(Guid id, CancellationToken cancellationToken = default);
    public Task<Artist?> GetArtistByIdAsync(Guid id, CancellationToken cancellationToken = default);
    public Task<List<Artist>> GetAllDeletedArtistAsync(CancellationToken cancellationToken = default);
}