using HomeLib.Core;
using HomeLib.Core.Exceptions;
using HomeLib.Core.Interfaces;
using HomeLib.Core.Interfaces.ForRepositories;
using HomeLib.Core.Model;

namespace HomeLib.Services.Services;

public class PlaylistService(IPlaylistRepository playlistRepository) : IPlaylistService
{
    public async Task<List<UserPlaylists>> GetAllPlaylists(Guid userId)
    {
        return await playlistRepository.GetAllPlaylistsAsync(userId);
    }

    public async Task<UserPlaylists> GetPlaylistById(Guid userId, Guid playlistId)
    {
        var playlist = await playlistRepository.GetPlaylistByIdAsync(userId, playlistId);
        return playlist ?? throw new NotFoundException("Playlist not found");
    }

    public async Task<UserPlaylists> AddPlaylist(Guid userId, string name)
    {
        var playlist = new UserPlaylists
        {
            Id = Guid.NewGuid(),
            Name = name,
            UserId = userId,
            CreatedAt = DateTime.UtcNow,
        };

        await playlistRepository.AddPlaylistsAsync(playlist);
        return playlist;
    }

    public async Task DeletePlaylist(Guid userId, Guid playlistId)
    {
        await GetPlaylistById(userId, playlistId);
        await playlistRepository.DeletePlaylistsAsync(userId, playlistId);
    }

    public async Task UpdatePlaylist(Guid userId, Guid playlistId, string name)
    {
        var playlist = await GetPlaylistById(userId, playlistId);

        playlist.Name = name;
        await playlistRepository.UpdatePlaylistsAsync(playlist);
    }

    public async Task AddTrack(Guid userId, Guid playlistId, Guid trackId)
    {
        var playlist = await playlistRepository.GetPlaylistByIdAsync(userId, playlistId);
        if (playlist == null)
        {
            throw new NotFoundException("Playlist not found");
        }

        var maxPosition = await playlistRepository.GetMaxPositionAsync(playlistId);

        var playlistTrack = new PlaylistTracks()
        {
            PlaylistId = playlistId,
            TrackId = trackId,
            Position = maxPosition + 1,
            CreatedAt = DateTime.UtcNow,
        };

        await playlistRepository.AddTrackAsync(playlistTrack);
    }

    public async Task DeleteTrack(Guid userId, Guid playlistId, Guid trackId)
    {
        var track = await playlistRepository.GetTrackByIdAsync(userId, playlistId, trackId);
        if (track == null) throw new NotFoundException($"Track with id: {trackId}, not found");

        await playlistRepository.DeleteTrackAsync(userId, playlistId, trackId);
    }
}