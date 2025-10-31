using HomeLib.Core;
using HomeLib.Core.Interfaces.ForRepositories;
using HomeLib.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace HomeLib.Infrastructure.Repositories;

public class TracksRepository(HomeLibDbContext context) : ITrackRepository
{
    public async Task<List<Track>> GetAllTracksAsync()
    {
        return await context.Tracks.AsNoTracking()
            .Include(t => t.Artist)
            .Include(t => t.Album)
            .ToListAsync();
    }

    public Task<Track?> GetTrackByIdAsync(Guid id)
    {
        return context.Tracks
            .Include(t => t.Artist)
            .Include(t => t.Album)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task UpdateTrackAsync(Track track)
    {
        context.Tracks.Update(track);
        await context.SaveChangesAsync();
    }

    public async Task AddTrackAsync(Track track)
    {
        context.Tracks.Add(track);
        await context.SaveChangesAsync();
    }

    public async Task DeleteTrackAsync(Guid id)
    {
        var track = await context.Tracks.FindAsync(id);
        if (track != null)
        {
            context.Tracks.Remove(track);
            await context.SaveChangesAsync();
        }
    }
}