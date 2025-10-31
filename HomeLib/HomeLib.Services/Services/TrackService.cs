using HomeLib.Core;
using HomeLib.Core.Exceptions;
using HomeLib.Core.Interfaces;
using HomeLib.Core.Interfaces.ForRepositories;
using HomeLib.Core.Model;

namespace HomeLib.Services.Services;

public class TrackService(ITrackRepository trackRepository) : ITrackService
{
    public async Task<List<Track>> GetAllTracks()
    {
        return await trackRepository.GetAllTracksAsync();
    }

    public async Task<Track> GetTrackById(Guid id)
    {
        if(id == Guid.Empty) throw new BadRequestException("Invalid track id");
        var track = await trackRepository.GetTrackByIdAsync(id);
        
        return track ?? throw new NotFoundException($"Track with {id} not found");
    }

    public async Task AddTrack(string name, Guid albumId, Guid artistId, int duration)
    {
        var track = new Track()
        {
            Id = Guid.NewGuid(),
            Name = name,
            Duration = duration,
            AlbumId = albumId,
            ArtistId = artistId,
        };

        await trackRepository.AddTrackAsync(track);
    }

    public async Task DeleteTrack(Guid id)
    {
        if(id == Guid.Empty) throw new BadRequestException("Invalid track id");
        var track = await trackRepository.GetTrackByIdAsync(id);
        if (track == null) throw new NotFoundException($"Track with {id} not found");
        
        await trackRepository.DeleteTrackAsync(id);
    }

    public async Task UpdateTrack(Guid id, string name, int duration)
    {
        if(id == Guid.Empty) throw new BadRequestException("Invalid track id");
        var track = await trackRepository.GetTrackByIdAsync(id);
        if (track == null) throw new NotFoundException($"Track with {id} not found");

        track.Name = name;
        track.Duration = duration;
        
        await trackRepository.UpdateTrackAsync(track);
    }
}