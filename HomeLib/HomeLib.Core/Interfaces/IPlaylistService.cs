using HomeLib.Core.Model;

namespace HomeLib.Core.Interfaces;

public interface IPlaylistService
{
    public Task<List<UserPlaylists>> GetAllPlaylists(int page, int pageSize, Guid userId,
        CancellationToken cancellationToken);

    public Task<UserPlaylists> GetPlaylistById(Guid userId, Guid playlistId, CancellationToken cancellationToken);
    public Task<UserPlaylists> AddPlaylist(Guid userId, string name, CancellationToken cancellationToken);
    public Task HardDeletePlaylist(Guid userId, Guid playlistId, CancellationToken cancellationToken);
    public Task UpdatePlaylist(Guid userId, Guid playlistId, string name, CancellationToken cancellationToken);
    public Task DeleteTrack(Guid userId, Guid playlistId, Guid trackId, CancellationToken cancellationToken);
    public Task AddTrack(Guid userId, Guid playlistId, Guid trackId, CancellationToken cancellationTokenі);
}