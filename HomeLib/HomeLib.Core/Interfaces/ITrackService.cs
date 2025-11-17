using HomeLib.Core.Model;

namespace HomeLib.Core.Interfaces;

public interface ITrackService
{
    public Task<List<Track>> GetAllTracks();
    public Task<Track> GetTrackById(Guid id);
    public Task AddTrack(string name, Guid albumId, Guid artistId, int duration);
    public Task HardDeleteTrack(Guid id);
    public Task SoftDeleteTrack(Guid id);
    public Task UpdateTrack(Guid id, string name, int duration);
    public Task<List<Track>> GetNewTracks(int count);
}