using HomeLib.Core;
using HomeLib.Core.Interfaces;
using HomeLib.Core.Interfaces.ForRepositories;
using HomeLib.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace HomeLib.Infrastructure.Repositories;

public class ArtistsRepository(HomeLibDbContext context) : IArtistRepository
{
    public async Task<List<Artist>> GetAllArtistAsync()
    {
        return await context.Artists
            .Include(a => a.Albums)
            .Include(t => t.Tracks)
            .ToListAsync();
    }

    public async Task<Artist?> GetArtistByIdAsync(Guid id)
    {
        return await context.Artists
            .Include(a => a.Albums)
            .Include(t => t.Tracks)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddArtistAsync(Artist artist)
    {
        await context.Artists.AddAsync(artist);
        await context.SaveChangesAsync();
    }

    public async Task UpdateArtistAsync(Artist artist)
    {
        context.Artists.Update(artist);
        await context.SaveChangesAsync();
    }

    public async Task DeleteArtistAsync(Guid id)
    {
        var artist = context.Artists.FindAsync(id);
        context.Artists.Remove(await artist);
        await context.SaveChangesAsync();
    }
}