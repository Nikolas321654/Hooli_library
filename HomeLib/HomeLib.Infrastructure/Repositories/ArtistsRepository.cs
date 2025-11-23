using HomeLib.Core;
using HomeLib.Core.Interfaces;
using HomeLib.Core.Interfaces.ForRepositories;
using HomeLib.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace HomeLib.Infrastructure.Repositories;

public class ArtistsRepository(HomeLibDbContext context) : IArtistRepository
{
    public async Task<List<Artist>> GetAllArtistAsync(CancellationToken cancellationToken = default)
    {
        return await context.Artists.AsNoTracking()
            .Include(a => a.Albums)
            .Include(a => a.TrackArtists)
            .ThenInclude(ta => ta.Track)
            .Where(a => a.IsDeleted == false)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Artist>> GetAllDeletedArtistAsync(CancellationToken cancellationToken = default)
    {
        return await context.Artists.AsNoTracking()
            .Include(a => a.Albums)
            .Include(a => a.TrackArtists)
            .ThenInclude(ta => ta.Track)
            .Where(a => a.IsDeleted == true)
            .ToListAsync(cancellationToken);
    }

    public async Task<Artist?> GetArtistByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Artists
            .Include(a => a.Albums)
            .Include(t => t.TrackArtists)
            .ThenInclude(ta => ta.Track)
            .FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false, cancellationToken);
    }

    public async Task AddArtistAsync(Artist artist, CancellationToken cancellationToken = default)
    {
        await context.Artists.AddAsync(artist, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateArtistAsync(Artist artist, CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task HardDeleteArtistAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var artist = await context.Artists.FindAsync([id], cancellationToken);
        if (artist != null)
        {
            context.Artists.Remove(artist);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}