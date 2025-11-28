using HomeLib.Core;
using HomeLib.Core.Exceptions;
using HomeLib.Core.Interfaces;
using HomeLib.Core.Interfaces.ForRepositories;
using HomeLib.Core.Model;

namespace HomeLib.Services.Services;

public class TrackService(ITrackRepository trackRepository) : ITrackService
{
    private async Task<Track> CheckTrackExists(Guid id, CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty) throw new BadRequestException("Invalid track id");
        var track = await trackRepository.GetTrackByIdAsync(id, cancellationToken);
        return track ?? throw new NotFoundException($"Track with {id} not found");
    }

    public async Task<List<Track>> GetAllTracks(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 1;
        if (pageSize > 100) pageSize = 100;
        return await trackRepository.GetAllTracksAsync(page, pageSize, cancellationToken);
    }

    public async Task<Track> GetTrackById(Guid id, CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty) throw new BadRequestException("Invalid track id");
        var track = await trackRepository.GetTrackByIdAsync(id, cancellationToken);

        return track ?? throw new NotFoundException($"Track with {id} not found");
    }

    public async Task<Track> AddTrack(string name, Guid albumId, Guid artistId, int duration,
        CancellationToken cancellationToken = default)
    {
        var track = new Track()
        {
            Id = Guid.NewGuid(),
            Name = name,
            Duration = duration,
            AlbumId = albumId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        var trackArtist = new TracksArtists()
        {
            ArtistId = artistId,
            TrackId = track.Id,
            UpdatedAt = track.UpdatedAt,
        };
        await trackRepository.AddTrackAsync(track, trackArtist, cancellationToken);

        return track;
    }

    public async Task HardDeleteTrack(Guid id, CancellationToken cancellationToken = default)
    {
        await CheckTrackExists(id, cancellationToken);
        await trackRepository.DeleteTrackArtistAsync(id, cancellationToken);
        await trackRepository.DeleteTrackAsync(id, cancellationToken);
    }


    public async Task SoftDeleteTrack(Guid id, CancellationToken cancellationToken = default)
    {
        var track = await CheckTrackExists(id, cancellationToken);

        track.IsDeleted = true;
        track.UpdatedAt = DateTime.UtcNow;

        await trackRepository.UpdateTrackAsync(track, cancellationToken);
    }

    public async Task UpdateTrack(Guid id, string name, int duration, CancellationToken cancellationToken = default)
    {
        var track = await CheckTrackExists(id, cancellationToken);

        track.Name = name;
        track.Duration = duration;
        track.UpdatedAt = DateTime.UtcNow;

        await trackRepository.UpdateTrackAsync(track, cancellationToken);
    }

    public async Task<List<Track>> GetNewTracks(int count, CancellationToken cancellationToken = default)
    {
        return await trackRepository.GetNewTracks(count, cancellationToken);
    }
}