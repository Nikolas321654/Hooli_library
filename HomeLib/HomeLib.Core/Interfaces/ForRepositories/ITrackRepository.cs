using HomeLib.Core.Model;

namespace HomeLib.Core.Interfaces.ForRepositories;

public interface ITrackRepository
{
    public Task<List<Track>> GetAllTracksAsync(CancellationToken cancellationToken = default);
    public Task<Track?> GetTrackByIdAsync(Guid id, CancellationToken cancellationToken = default);
    public Task<List<Track>> GetAllDeletedTracksAsync(CancellationToken cancellationToken = default);
    public Task AddTrackAsync(Track track, TracksArtists trackArtist, CancellationToken cancellationToken = default);
    public Task DeleteTrackAsync(Guid id, CancellationToken cancellationToken = default);
    public Task UpdateTrackAsync(Track track, CancellationToken cancellationToken = default);
    public Task<TracksArtists?> DeleteTrackArtistAsync(Guid id, CancellationToken cancellationToken = default);
    public Task<List<Track>> GetNewTracks(int count, CancellationToken cancellationToken = default);
}