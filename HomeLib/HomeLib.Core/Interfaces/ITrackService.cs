using HomeLib.Core.Model;

namespace HomeLib.Core.Interfaces;

public interface ITrackService
{
    public Task<List<Track>> GetAllTracks(CancellationToken cancellationToken);
    public Task<Track> GetTrackById(Guid id, CancellationToken cancellationToken);

    public Task<Track> AddTrack(string name, Guid albumId, Guid artistId, int duration,
        CancellationToken cancellationToken);

    public Task HardDeleteTrack(Guid id, CancellationToken cancellationToken);
    public Task SoftDeleteTrack(Guid id, CancellationToken cancellationToken);
    public Task UpdateTrack(Guid id, string name, int duration, CancellationToken cancellationToken);
    public Task<List<Track>> GetNewTracks(int count, CancellationToken cancellationToken);
}