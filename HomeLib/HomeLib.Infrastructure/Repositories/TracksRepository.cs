using HomeLib.Core;
using HomeLib.Core.Interfaces.ForRepositories;
using HomeLib.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace HomeLib.Infrastructure.Repositories;

public class TracksRepository(HomeLibDbContext context) : ITrackRepository
{
    public async Task<List<Track>> GetAllTracksAsync(CancellationToken cancellationToken = default)
    {
        return await context.Tracks.AsNoTracking()
            .Include(t => t.TrackArtists)
            .ThenInclude(ta => ta.Artist)
            .Include(t => t.Album)
            .Where(t => t.IsDeleted == false)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Track>> GetAllDeletedTracksAsync(CancellationToken cancellationToken = default)
    {
        return await context.Tracks.AsNoTracking()
            .Include(t => t.TrackArtists)
            .ThenInclude(ta => ta.Artist)
            .Include(t => t.Album)
            .Where(t => t.IsDeleted == true)
            .ToListAsync(cancellationToken);
    }

    public Task<Track?> GetTrackByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return context.Tracks
            .Include(t => t.TrackArtists)
            .ThenInclude(ta => ta.Artist)
            .Include(t => t.Album)
            .FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false, cancellationToken);
    }

    public async Task UpdateTrackAsync(Track track, CancellationToken cancellationToken = default)
    {
        context.Tracks.Update(track);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task AddTrackAsync(Track track, TracksArtists tracksArtists,
        CancellationToken cancellationToken = default)
    {
        context.Tracks.Add(track);
        context.TracksArtists.Add(tracksArtists);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteTrackAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var track = await context.Tracks.FindAsync([id], cancellationToken);
        if (track != null)
        {
            context.Tracks.Remove(track);
            await context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<TracksArtists?> DeleteTrackArtistAsync(Guid trackId,
        CancellationToken cancellationToken = default)
    {
        var links = await context.TracksArtists
            .Where(x => x.TrackId == trackId)
            .ToListAsync(cancellationToken);

        if (links.Count == 0) return null;

        var first = links[0];
        context.TracksArtists.RemoveRange(links);
        await context.SaveChangesAsync(cancellationToken);
        return first;
    }

    public async Task<List<Track>> GetNewTracks(int count, CancellationToken cancellationToken = default)
    {
        return await context.Tracks.AsNoTracking()
            .Include(t => t.TrackArtists)
            .ThenInclude(tr => tr.Artist)
            .Include(t => t.Album)
            .Where(t => t.IsDeleted == false)
            .OrderByDescending(t => t.CreatedAt)
            .Take(count)
            .ToListAsync(cancellationToken);
    }
}