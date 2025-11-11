using HomeLib.Core.Model;

namespace HomeLib.Core.Interfaces.ForRepositories;

public interface IPlaylistRepository
{
    public Task<List<UserPlaylists>> GetAllPlaylistsAsync(Guid userId);
    public Task<UserPlaylists?> GetPlaylistByIdAsync(Guid userId, Guid playlistId);
    public Task AddPlaylistsAsync(UserPlaylists playlistTracks);
    public Task DeletePlaylistsAsync(Guid userId, Guid playlistId);
    public Task UpdatePlaylistsAsync(UserPlaylists playlistTracks);
    public Task<List<PlaylistTracks>> GetAllTracksAsync(Guid userId, Guid playlistId);
    public Task<PlaylistTracks?> GetTrackByIdAsync(Guid userId, Guid playlistId, Guid trackId);
    public Task AddTrackAsync(PlaylistTracks track);
    public Task DeleteTrackAsync(Guid userId, Guid playlistId, Guid trackId);
    public Task<int> GetMaxPositionAsync(Guid playlistId);
}