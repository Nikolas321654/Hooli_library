using HomeLib.Core;
using HomeLib.Core.Exceptions;
using HomeLib.Core.Interfaces;
using HomeLib.Core.Interfaces.ForRepositories;
using HomeLib.Core.Model;

namespace HomeLib.Services.Services;

public class PlaylistService(IPlaylistRepository playlistRepository) : IPlaylistService
{
    public async Task<List<UserPlaylists>> GetAllPlaylists(Guid userId, CancellationToken cancellationToken = default)
    {
        return await playlistRepository.GetAllPlaylistsAsync(userId, cancellationToken);
    }

    public async Task<UserPlaylists> GetPlaylistById(Guid userId, Guid playlistId,
        CancellationToken cancellationToken = default)
    {
        var playlist = await playlistRepository.GetPlaylistByIdAsync(userId, playlistId, cancellationToken);
        return playlist ?? throw new NotFoundException($"Playlist with id: {playlistId}, not found");
    }

    public async Task<UserPlaylists> AddPlaylist(Guid userId, string name,
        CancellationToken cancellationToken = default)
    {
        var playlist = new UserPlaylists
        {
            Id = Guid.NewGuid(),
            Name = name,
            UserId = userId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await playlistRepository.AddPlaylistsAsync(playlist, cancellationToken);
        return playlist;
    }

    public async Task HardDeletePlaylist(Guid userId, Guid playlistId, CancellationToken cancellationToken = default)
    {
        await GetPlaylistById(userId, playlistId, cancellationToken);
        await playlistRepository.DeletePlaylistsAsync(userId, playlistId, cancellationToken);
    }

    public async Task UpdatePlaylist(Guid userId, Guid playlistId, string name,
        CancellationToken cancellationToken = default)
    {
        if (playlistId == Guid.Empty) throw new BadRequestException("Invalid playlist id");
        if (name == null) throw new BadRequestException("Invalid playlist name");
        var playlist = await playlistRepository.GetPlaylistByIdAsync(userId, playlistId, cancellationToken);
        if (playlist == null) throw new NotFoundException($"Playlist with id: {playlistId}, not found");

        playlist.Name = name;
        playlist.UpdatedAt = DateTime.UtcNow;
        await playlistRepository.UpdatePlaylistsAsync(playlist, cancellationToken);
    }

    public async Task AddTrack(Guid userId, Guid playlistId, Guid trackId,
        CancellationToken cancellationToken = default)
    {
        var playlist = await playlistRepository.GetPlaylistByIdAsync(userId, playlistId, cancellationToken);
        if (await playlistRepository.GetTrackByIdAsync(trackId, cancellationToken) == null)
            throw new NotFoundException($"Track with id: {trackId}, not found");

        if (playlist == null)
            throw new NotFoundException("Playlist not found");

        if (await playlistRepository.GetPlaylistTrackByIdAsync(userId, playlistId, trackId, cancellationToken) != null)
            throw new AlreadyAddedException("Track already exists");

        var maxPosition = await playlistRepository.GetMaxPositionAsync(playlistId, cancellationToken);

        var playlistTrack = new PlaylistTracks()
        {
            PlaylistId = playlistId,
            TrackId = trackId,
            Position = maxPosition + 1,
            CreatedAt = DateTime.UtcNow,
        };

        await playlistRepository.AddTrackAsync(playlistTrack, cancellationToken);
    }

    public async Task DeleteTrack(Guid userId, Guid playlistId, Guid trackId,
        CancellationToken cancellationToken = default)
    {
        var track = await playlistRepository.GetPlaylistTrackByIdAsync(userId, playlistId, trackId, cancellationToken);
        if (track == null) throw new NotFoundException($"Track with id: {trackId}, not found");

        await playlistRepository.DeleteTrackAsync(userId, playlistId, trackId, cancellationToken);
    }
}