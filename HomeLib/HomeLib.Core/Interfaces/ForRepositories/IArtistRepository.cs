namespace HomeLib.Core.Interfaces.ForRepositories;

public interface IArtistRepository
{
    public Task<List<Artist>> GetAllArtistAsync();
    public Task AddArtistAsync(Artist artist);
    public Task UpdateArtistAsync(Artist artist);
    public Task DeleteArtistAsync(Artist artist);
    public Task<Artist?> GetArtistByIdAsync(Guid id);
}