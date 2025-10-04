using HomeLib.Core;
using HomeLib.Core.Interfaces;
using HomeLib.Core.Interfaces.ForRepositories;

namespace HomeLib.Services.Services;

public class TrackService(ITrackRepository usersRepository) : ITrackService
{
    public async Task<List<Track>> GetAllTracks()
    {
        return await usersRepository.GetAllTracksAsync();
    }

    public async Task AddTrack(Track track)
    {
        await usersRepository.AddTrackAsync(track);
    }
}