using HomeLib.Core;
using HomeLib.Core.Interfaces;
using HomeLib.Core.Interfaces.ForRepositories;
using Microsoft.EntityFrameworkCore;

namespace HomeLib.Infrastructure.Repositories;

public class ArtistsRepository(HomeLibDbContext context) : IArtistRepository
{
    public async Task<List<Artist>> GetAllArtistAsync()
    {
        return await context.Artists.ToListAsync();
    }

    public async Task AddArtistAsync(Artist artist)
    {
        await context.Artists.AddAsync(artist);
        await context.SaveChangesAsync();
    }

    public async Task UpdateArtistAsync(Artist artist)
    {
        context.Update(artist);
        await context.SaveChangesAsync();
    }

    public async Task DeleteArtistAsync(Artist artist)
    {
        context.Artists.Remove(artist);
        await context.SaveChangesAsync();
    }

    public async Task<Artist?> GetArtistByIdAsync(Guid id)
    {
        return await context.Artists.FirstOrDefaultAsync(x => x.Id == id);
    }
}