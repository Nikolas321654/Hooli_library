namespace HomeLib.Core.Interfaces;

public interface ITrackService
{
    public Task<List<Track>> GetAllTracks();
    public Task AddTrack(Track track);
}