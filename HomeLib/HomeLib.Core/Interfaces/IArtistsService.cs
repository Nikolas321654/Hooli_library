using HomeLib.Core.Model;

namespace HomeLib.Core.Interfaces;

public interface IArtistsService
{
    public Task<List<Artist>> GetAllArtists();
    public Task<Artist?> GetArtistById(Guid id);
    public Task UpdateArtist(string name, bool grammy, Guid id);
    public Task AddArtist(string name, bool grammy);
    public Task DeleteArtist(Guid id);
}