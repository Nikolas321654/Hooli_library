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

    public async Task<bool> ExistingAsync(Guid id)
    {
        return await context.Artists.AsNoTracking().AnyAsync(a => a.Id == id);
    }

    public async Task AddArtistAsync(string name, bool grammy)
    {
        var artist = new Artist()
        {
            Name = name,
            Grammy = grammy
        };
        await context.Artists.AddAsync(artist);
        await context.SaveChangesAsync();
    }

    public async Task UpdateArtistAsync(string name, bool grammy, Guid id)
    {
        var artist = await context.Artists.FindAsync(id);
        if (artist != null)
        {
            artist.Name = name;
            artist.Grammy = grammy;
            await context.SaveChangesAsync();
        }
    }

    public async Task DeleteArtistAsync(Guid id)
    {
        var artist = context.Artists.FindAsync(id);
        context.Artists.Remove(await artist);
        await context.SaveChangesAsync();
    }

    public async Task<Artist?> GetArtistByIdAsync(Guid id)
    {
        return await context.Artists.FirstOrDefaultAsync(x => x.Id == id);
    }
}