namespace HomeLib.Core.Interfaces;

public interface IArtistsService
{
    public Task<List<Artist>> GetAllArtists();
    public Task AddArtist(Artist artist);
}