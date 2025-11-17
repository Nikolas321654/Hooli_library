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
        if (id == Guid.Empty) throw new BadRequestException("Invalid track id");
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
            CreatedAt =  DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        var trackArtist = new TracksArtists()
        {
            ArtistId = artistId,
            TrackId = track.Id,
            UpdatedAt = track.UpdatedAt,
        };
        await trackRepository.AddTrackAsync(track, trackArtist);
    }

    public async Task HardDeleteTrack(Guid id)
    {
        if (id == Guid.Empty) throw new BadRequestException("Invalid track id");
        var track = await trackRepository.GetTrackByIdAsync(id);
        if (track == null) throw new NotFoundException($"Track with {id} not found");
        var trackInTrackArtist = await trackRepository.DeleteTrackArtistAsync(id);
        if (trackInTrackArtist == null) throw new NotFoundException($"Track with {id} not found");

        trackInTrackArtist.IsDeleted = true;

        await trackRepository.DeleteTrackAsync(id);
    }

    public async Task SoftDeleteTrack(Guid id)
    {
        if (id == Guid.Empty) throw new BadRequestException("Invalid track id");
        var track = await trackRepository.GetTrackByIdAsync(id);
        if (track == null) throw new NotFoundException($"Track with {id} not found");

        track.IsDeleted = true;
        track.UpdatedAt = DateTime.UtcNow;

        await trackRepository.UpdateTrackAsync(track);
    }

    public async Task UpdateTrack(Guid id, string name, int duration)
    {
        if (id == Guid.Empty) throw new BadRequestException("Invalid track id");
        var track = await trackRepository.GetTrackByIdAsync(id);
        if (track == null) throw new NotFoundException($"Track with {id} not found");

        track.Name = name;
        track.Duration = duration;
        track.UpdatedAt = DateTime.UtcNow;

        await trackRepository.UpdateTrackAsync(track);
    }

    public async Task<List<Track>> GetNewTracks(int count)
    {
        return await trackRepository.GetNewTracks(count);
    }
}