using HomeLib.Core;
using HomeLib.Core.Interfaces.ForRepositories;
using HomeLib.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace HomeLib.Infrastructure.Repositories;

public class TracksRepository(HomeLibDbContext context) : ITrackRepository
{
    public async Task<List<Track>> GetAllTracksAsync()
    {
        return await context.Tracks.Include(t => t.TrackArtists)
            .ThenInclude(ta => ta.Artist)
            .Include(t => t.Album)
            .Where(t => t.IsDeleted == false)
            .ToListAsync();
    }

    public async Task<List<Track>> GetAllDeletedTracksAsync()
    {
        return await context.Tracks.AsNoTracking()
            .Include(t => t.TrackArtists)
            .ThenInclude(ta => ta.Artist)
            .Include(t => t.Album)
            .Where(t => t.IsDeleted == true)
            .ToListAsync();
    }

    public Task<Track?> GetTrackByIdAsync(Guid id)
    {
        return context.Tracks
            .Include(t => t.TrackArtists)
            .Include(t => t.Album)
            .FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
    }

    public async Task UpdateTrackAsync(Track track)
    {
        context.Tracks.Update(track);
        await context.SaveChangesAsync();
    }

    public async Task AddTrackAsync(Track track, TracksArtists tracksArtists)
    {
        context.Tracks.Add(track);
        context.TracksArtists.Add(tracksArtists);
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

    public async Task<TracksArtists?> DeleteTrackArtistAsync(Guid id)
    {
        return await context.TracksArtists.FindAsync(id);
    }

    public async Task<List<Track>> GetNewTracks(int count)
    {
        return await context.Tracks.Include(t => t.TrackArtists)
            .ThenInclude(tr => tr.Artist)
            .Include(t => t.Album)
            .Where(t => t.IsDeleted == false)
            .OrderByDescending(t => t.CreatedAt)
            .Take(count)
            .ToListAsync();
    }
}