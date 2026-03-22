using HomeLib.Core.Application.Common.Interfaces;

namespace HomeLib.Core.Application.Artists.Queries;

public record GetAllArtistsQuery(int Page, int PageSize) : IQuery<List<ArtistDto>>;

public record GetArtistByIdQuery(Guid Id) : IQuery<ArtistDetailsDto>;

public record GetArtistAlbumsQuery(Guid Id, int Page, int PageSize) : IQuery<ArtistAlbumsDto>;

public record GetArtistTracksQuery(Guid Id, int Page, int PageSize) : IQuery<ArtistTracksDto>;

public record GetArtistWithAlbumsTracksQuery(Guid Id, int Page, int PageSize) : IQuery<ArtistAlbumsTracksDto>;

public record ArtistDto(Guid ArtistId, string Name, bool Grammy);

public record ArtistDetailsDto(Guid ArtistId, string Name, bool Grammy);

public record AlbumForArtistDto(Guid AlbumId, string Name, int Year);

public record TrackForArtistDto(Guid TrackId, string Name, int Duration);

public record ArtistAlbumsDto(Guid ArtistId, string Name, bool Grammy, List<AlbumForArtistDto> Albums);

public record ArtistTracksDto(Guid ArtistId, string Name, bool Grammy, List<TrackForArtistDto> Tracks);

public record ArtistAlbumsTracksDto(Guid ArtistId, string Name, bool Grammy, List<AlbumForArtistDto> Albums, List<TrackForArtistDto> Tracks);
