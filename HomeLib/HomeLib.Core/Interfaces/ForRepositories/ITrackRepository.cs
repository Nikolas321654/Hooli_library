using HomeLib.Core.Model;

namespace HomeLib.Core.Interfaces.ForRepositories;

public interface ITrackRepository
{
    public Task<List<Track>> GetAllTracksAsync();
    public Task<Track?> GetTrackByIdAsync(Guid id);
    public Task<List<Track>> GetAllDeletedTracksAsync();
    public Task AddTrackAsync(Track track, TracksArtists trackArtist);
    public Task DeleteTrackAsync(Guid id);
    public Task UpdateTrackAsync(Track track);
    public Task<TracksArtists?> DeleteTrackArtistAsync(Guid id);
    public Task<List<Track>> GetNewTracks(int count);
}