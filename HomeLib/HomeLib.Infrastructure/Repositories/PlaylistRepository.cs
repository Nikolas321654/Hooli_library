using HomeLib.Core;
using HomeLib.Core.Interfaces.ForRepositories;
using HomeLib.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace HomeLib.Infrastructure.Repositories;

public class PlaylistRepository(HomeLibDbContext context) : IPlaylistRepository
{
    public async Task<List<UserPlaylists>> GetAllPlaylistsAsync(Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await context.UserPlaylists.AsNoTracking()
            .Where(x => x.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<UserPlaylists?> GetPlaylistByIdAsync(Guid userId, Guid playlistId,
        CancellationToken cancellationToken = default)
    {
        return await context.UserPlaylists
            .Where(x => x.UserId == userId && x.Id == playlistId)
            .Include(t => t.PlaylistTracks)
            .ThenInclude(pt => pt.Track)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task AddPlaylistsAsync(UserPlaylists playlistTracks, CancellationToken cancellationToken = default)
    {
        await context.AddAsync(playlistTracks, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeletePlaylistsAsync(Guid userId, Guid playlistId, CancellationToken cancellationToken = default)
    {
        var playlist = await GetPlaylistByIdAsync(userId, playlistId, cancellationToken);
        if (playlist != null)
        {
            context.Remove(playlist);
            await context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task UpdatePlaylistsAsync(UserPlaylists playlistTracks, CancellationToken cancellationToken = default)
    {
        context.Update(playlistTracks);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<PlaylistTracks?> GetPlaylistTrackByIdAsync(Guid userId, Guid playlistId, Guid trackId,
        CancellationToken cancellationToken = default)
    {
        return await context.PlaylistTracks
            .Where(x => x.PlaylistId == playlistId
                        && x.UserPlaylists.UserId == userId
                        && x.Track.IsDeleted == false)
            .Include(t => t.Track)
            .FirstOrDefaultAsync(x => x.TrackId == trackId, cancellationToken);
    }

    public async Task AddTrackAsync(PlaylistTracks track, CancellationToken cancellationToken = default)
    {
        await context.PlaylistTracks.AddAsync(track, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteTrackAsync(Guid userId, Guid playlistId, Guid trackId,
        CancellationToken cancellationToken = default)
    {
        var track = await context.PlaylistTracks
            .Where(x =>
                x.PlaylistId == playlistId
                && x.TrackId == trackId
                && x.Track.IsDeleted == false
                && x.UserPlaylists.UserId == userId)
            .FirstOrDefaultAsync(cancellationToken);

        if (track != null)
        {
            var position = track.Position;
            context.PlaylistTracks.Remove(track);

            var tracksToUpdate = await context.PlaylistTracks
                .Where(x => x.PlaylistId == playlistId && x.Position > position)
                .ToListAsync(cancellationToken);

            foreach (var t in tracksToUpdate)
            {
                t.Position--;
            }

            await context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<int> GetMaxPositionAsync(Guid playlistId, CancellationToken cancellationToken = default)
    {
        var maxPosition = await context.PlaylistTracks
            .Where(pt => pt.PlaylistId == playlistId)
            .MaxAsync(pt => (int?)pt.Position, cancellationToken);

        return maxPosition ?? 0;
    }

    public async Task<Track?> GetTrackByIdAsync(Guid trackId, CancellationToken cancellationToken = default)
    {
        return await context.Tracks.Where(t => t.IsDeleted == false)
            .FirstOrDefaultAsync(x => x.Id == trackId, cancellationToken);
    }
}