namespace HomeLib.Core.Interfaces;

public interface IArtistsService
{
    public Task<List<Artist>> GetAllArtists();
    public Task<Artist?> GetArtistById(Guid id);
    public Task UpdateArtist(Artist artist);
    public Task AddArtist(Artist artist);
    public Task DeleteArtist(Artist artist);
}