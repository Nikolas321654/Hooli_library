using HomeLib.Core.Application.Common.Interfaces;
using HomeLib.Core.Application.Artists.Queries;
using HomeLib.Core.Exceptions;
using HomeLib.Core.Interfaces.ForRepositories;

namespace HomeLib.Core.Application.Artists.Handlers;

public class GetAllArtistsQueryHandler(IArtistRepository artistRepository) 
    : IQueryHandler<GetAllArtistsQuery, List<ArtistDto>>
{
    public async Task<List<ArtistDto>> Handle(GetAllArtistsQuery query, CancellationToken cancellationToken)
    {
        var page = query.Page < 1 ? 1 : query.Page;
        var pageSize = query.PageSize < 1 ? 1 : (query.PageSize > 100 ? 100 : query.PageSize);
        
        var artists = await artistRepository.GetAllArtistAsync(page, pageSize, cancellationToken);
        return artists.Select(a => new ArtistDto(a.Id, a.Name, a.Grammy)).ToList();
    }
}

public class GetArtistByIdQueryHandler(IArtistRepository artistRepository) 
    : IQueryHandler<GetArtistByIdQuery, ArtistDetailsDto>
{
    public async Task<ArtistDetailsDto> Handle(GetArtistByIdQuery query, CancellationToken cancellationToken)
    {
        if (query.Id == Guid.Empty) throw new BadRequestException("Id cannot be empty");
        var artist = await artistRepository.GetArtistByIdAsync(query.Id, cancellationToken);
        if (artist == null) throw new NotFoundException($"Artist with {query.Id} id not found");

        return new ArtistDetailsDto(artist.Id, artist.Name, artist.Grammy);
    }
}

public class GetArtistAlbumsQueryHandler(IArtistRepository artistRepository) 
    : IQueryHandler<GetArtistAlbumsQuery, ArtistAlbumsDto>
{
    public async Task<ArtistAlbumsDto> Handle(GetArtistAlbumsQuery query, CancellationToken cancellationToken)
    {
        if (query.Id == Guid.Empty) throw new BadRequestException("Id cannot be empty");
        var artist = await artistRepository.GetArtistByIdAsync(query.Id, cancellationToken);
        if (artist == null) throw new NotFoundException($"Artist with {query.Id} id not found");

        var page = query.Page < 1 ? 1 : query.Page;
        var pageSize = query.PageSize < 1 ? 1 : query.PageSize;

        var albums = artist.Albums
            .OrderBy(a => a.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(a => new AlbumForArtistDto(a.Id, a.Name, a.Year))
            .ToList();

        return new ArtistAlbumsDto(artist.Id, artist.Name, artist.Grammy, albums);
    }
}

public class GetArtistTracksQueryHandler(IArtistRepository artistRepository) 
    : IQueryHandler<GetArtistTracksQuery, ArtistTracksDto>
{
    public async Task<ArtistTracksDto> Handle(GetArtistTracksQuery query, CancellationToken cancellationToken)
    {
        if (query.Id == Guid.Empty) throw new BadRequestException("Id cannot be empty");
        var artist = await artistRepository.GetArtistByIdAsync(query.Id, cancellationToken);
        if (artist == null) throw new NotFoundException($"Artist with {query.Id} id not found");

        var page = query.Page < 1 ? 1 : query.Page;
        var pageSize = query.PageSize < 1 ? 1 : query.PageSize;

        var tracks = artist.TrackArtists
            .OrderBy(ta => ta.Track.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(ta => new TrackForArtistDto(ta.TrackId, ta.Track.Name, ta.Track.Duration))
            .ToList();

        return new ArtistTracksDto(artist.Id, artist.Name, artist.Grammy, tracks);
    }
}

public class GetArtistWithAlbumsTracksQueryHandler(IArtistRepository artistRepository) 
    : IQueryHandler<GetArtistWithAlbumsTracksQuery, ArtistAlbumsTracksDto>
{
    public async Task<ArtistAlbumsTracksDto> Handle(GetArtistWithAlbumsTracksQuery query, CancellationToken cancellationToken)
    {
        if (query.Id == Guid.Empty) throw new BadRequestException("Id cannot be empty");
        var artist = await artistRepository.GetArtistByIdAsync(query.Id, cancellationToken);
        if (artist == null) throw new NotFoundException($"Artist with {query.Id} id not found");

        var page = query.Page < 1 ? 1 : query.Page;
        var pageSize = query.PageSize < 1 ? 1 : query.PageSize;

        var albums = artist.Albums
            .OrderBy(a => a.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(a => new AlbumForArtistDto(a.Id, a.Name, a.Year))
            .ToList();

        var tracks = artist.TrackArtists
            .OrderBy(ta => ta.Track.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(ta => new TrackForArtistDto(ta.TrackId, ta.Track.Name, ta.Track.Duration))
            .ToList();

        return new ArtistAlbumsTracksDto(artist.Id, artist.Name, artist.Grammy, albums, tracks);
    }
}
