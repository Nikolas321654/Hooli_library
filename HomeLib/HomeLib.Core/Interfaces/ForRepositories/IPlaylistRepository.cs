using HomeLib.Core.Model;

namespace HomeLib.Core.Interfaces.ForRepositories;

public interface IPlaylistRepository
{
    public Task<List<UserPlaylists>> GetAllPlaylistsAsync(int page, int pageSize, Guid userId,
        CancellationToken cancellationToken = default);

    public Task<UserPlaylists?> GetPlaylistByIdAsync(Guid userId, Guid playlistId,
        CancellationToken cancellationToken = default);

    public Task AddPlaylistsAsync(UserPlaylists playlistTracks, CancellationToken cancellationToken = default);
    public Task DeletePlaylistsAsync(Guid userId, Guid playlistId, CancellationToken cancellationToken = default);
    public Task UpdatePlaylistsAsync(UserPlaylists playlistTracks, CancellationToken cancellationToken = default);

    public Task<PlaylistTracks?> GetPlaylistTrackByIdAsync(Guid userId, Guid playlistId, Guid trackId,
        CancellationToken cancellationToken = default);

    public Task AddTrackAsync(PlaylistTracks track, CancellationToken cancellationToken = default);

    public Task DeleteTrackAsync(Guid userId, Guid playlistId, Guid trackId,
        CancellationToken cancellationToken = default);

    public Task<int> GetMaxPositionAsync(Guid playlistId, CancellationToken cancellationToken = default);
    public Task<Track?> GetTrackByIdAsync(Guid trackId, CancellationToken cancellationToken = default);
}