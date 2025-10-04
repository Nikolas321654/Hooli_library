namespace HomeLib.Core.Interfaces.ForRepositories;

public interface ITrackRepository
{
    public Task<List<Track>> GetAllTracksAsync();
    public Task AddTrackAsync(Track track);
}