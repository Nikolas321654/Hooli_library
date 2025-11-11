using HomeLib.Core.Model;

namespace HomeLib.Core.Interfaces;

public interface IPlaylistService
{
    public Task<List<UserPlaylists>> GetAllPlaylists(Guid userId);
    public Task<UserPlaylists> GetPlaylistById(Guid userId, Guid playlistId);
    public Task<UserPlaylists> AddPlaylist(Guid userId, string name);
    public Task DeletePlaylist(Guid userId, Guid playlistId);
    public Task UpdatePlaylist(Guid userId, Guid playlistId, string name);
    public Task DeleteTrack(Guid userId, Guid playlistId, Guid trackId);
    public Task AddTrack(Guid userId, Guid playlistId, Guid trackId);
}