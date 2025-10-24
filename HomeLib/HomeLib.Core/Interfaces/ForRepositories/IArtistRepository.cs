namespace HomeLib.Core.Interfaces.ForRepositories;

public interface IArtistRepository
{
    public Task<List<Artist>> GetAllArtistAsync();
    public Task AddArtistAsync(string name, bool grammy);
    public Task UpdateArtistAsync(string name, bool grammy, Guid id);
    public Task DeleteArtistAsync(Guid id);
    public Task<bool> ExistingAsync(Guid id);
    public Task<Artist?> GetArtistByIdAsync(Guid id);
}