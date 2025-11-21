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
            .Include(a => a.TrackArtists)
            .ThenInclude(ta => ta.Track)
            .Where(a => a.IsDeleted == false)
            .ToListAsync();
    }

    public async Task<List<Artist>> GetAllDeletedArtistAsync()
    {
        return await context.Artists
            .Include(a => a.Albums)
            .Include(a => a.TrackArtists)
            .ThenInclude(ta => ta.Track)
            .Where(a => a.IsDeleted == true)
            .ToListAsync();
    }
    
    public async Task<Artist?> GetArtistByIdAsync(Guid id)
    {
        return await context.Artists
            .Include(a => a.Albums)
            .Include(t => t.TrackArtists)
            .ThenInclude(ta => ta.Track)
            .FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
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

    public async Task HardDeleteArtistAsync(Guid id)
    {
        var artist = await context.Artists.FindAsync(id);
        if (artist != null)
        {
            context.Artists.Remove(artist);
            await context.SaveChangesAsync();
        }
    }
}