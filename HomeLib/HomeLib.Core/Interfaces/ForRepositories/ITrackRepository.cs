using HomeLib.Core.Model;

namespace HomeLib.Core.Interfaces.ForRepositories;

public interface ITrackRepository
{
    public Task<List<Track>> GetAllTracksAsync();
    public Task<Track?> GetTrackByIdAsync(Guid id);
    public Task AddTrackAsync(Track track);
    public Task DeleteTrackAsync(Guid id);
    public Task UpdateTrackAsync(Track track);
}