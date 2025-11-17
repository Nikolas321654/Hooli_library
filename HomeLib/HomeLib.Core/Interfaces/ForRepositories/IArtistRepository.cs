using HomeLib.Core.Model;

namespace HomeLib.Core.Interfaces.ForRepositories;

public interface IArtistRepository
{
    public Task<List<Artist>> GetAllArtistAsync();
    public Task AddArtistAsync(Artist artist);
    public Task UpdateArtistAsync(Artist artist);
    public Task HardDeleteArtistAsync(Guid id);
    public Task<Artist?> GetArtistByIdAsync(Guid id);
    public Task<List<Artist>> GetAllDeletedArtistAsync();
}