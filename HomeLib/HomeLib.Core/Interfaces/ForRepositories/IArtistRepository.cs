namespace HomeLib.Core.Interfaces.ForRepositories;

public interface IArtistRepository
{
    public Task<List<Artist>> GetAllArtistAsync();
    public Task AddArtistAsync(Artist artist);
}