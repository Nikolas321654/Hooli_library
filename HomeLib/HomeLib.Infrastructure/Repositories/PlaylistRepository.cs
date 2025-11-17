using HomeLib.Core;
using HomeLib.Core.Interfaces.ForRepositories;
using HomeLib.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace HomeLib.Infrastructure.Repositories;

public class PlaylistRepository(HomeLibDbContext context) : IPlaylistRepository
{
    public async Task<List<UserPlaylists>> GetAllPlaylistsAsync(Guid userId)
    {
        return await context.UserPlaylists
            .Where(x => x.UserId == userId)
            .ToListAsync();
    }

    public async Task<UserPlaylists?> GetPlaylistByIdAsync(Guid userId, Guid playlistId)
    {
        return await context.UserPlaylists
            .Where(x => x.UserId == userId && x.Id == playlistId)
            .Include(t => t.PlaylistTracks)
            .ThenInclude(pt => pt.Track)
            .FirstOrDefaultAsync();
    }

    public async Task<List<PlaylistTracks>> GetAllTracksAsync(Guid userId, Guid playlistId)
    {
        return await context.PlaylistTracks
            .Where(x => x.PlaylistId == playlistId && x.UserPlaylists.UserId == userId)
            .Include(t => t.Track)
            .ToListAsync();
    }

    public async Task AddPlaylistsAsync(UserPlaylists playlistTracks)
    {
        await context.AddAsync(playlistTracks);
        await context.SaveChangesAsync();
    }

    public async Task DeletePlaylistsAsync(Guid userId, Guid playlistId)
    {
        var playlist = await GetPlaylistByIdAsync(userId, playlistId);
        if (playlist != null)
        {
            context.Remove(playlist);
            await context.SaveChangesAsync();
        }
    }

    public async Task UpdatePlaylistsAsync(UserPlaylists playlistTracks)
    {
        context.Update(playlistTracks);
        await context.SaveChangesAsync();
    }

    public async Task<PlaylistTracks?> GetPlaylistTrackByIdAsync(Guid userId, Guid playlistId, Guid trackId)
    {
        return await context.PlaylistTracks
            .Where(x => x.PlaylistId == playlistId && x.UserPlaylists.UserId == userId)
            .Include(t => t.Track)
            .FirstOrDefaultAsync(x => x.TrackId == trackId);
    }

    public async Task AddTrackAsync(PlaylistTracks track)
    {
        await context.PlaylistTracks.AddAsync(track);
        await context.SaveChangesAsync();
    }

    public async Task DeleteTrackAsync(Guid userId, Guid playlistId, Guid trackId)
    {
        var track = await context.PlaylistTracks
            .Where(x =>
                x.PlaylistId == playlistId
                && x.TrackId == trackId
                && x.UserPlaylists.UserId == userId)
            .FirstOrDefaultAsync();

        if (track != null)
        {
            var position = track.Position;
            context.PlaylistTracks.Remove(track);

            var tracksToUpdate = await context.PlaylistTracks
                .Where(x => x.PlaylistId == playlistId && x.Position > position)
                .ToListAsync();

            foreach (var t in tracksToUpdate)
            {
                t.Position--;
            }
            await context.SaveChangesAsync();
        }
    }

    public async Task<int> GetMaxPositionAsync(Guid playlistId)
    {
        var maxPosition = await context.PlaylistTracks
            .Where(pt => pt.PlaylistId == playlistId)
            .MaxAsync(pt => (int?)pt.Position);

        return maxPosition ?? 0;
    }

    public async Task<Track?> GetTrackByIdAsync(Guid trackId)
    {
        return await context.Tracks.FirstOrDefaultAsync(x => x.Id == trackId);
    }
}