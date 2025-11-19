using HomeLib.Core.Model;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace HomeLib.Tests;

public class TrackTest : IntegrationTestBase
{
    [Fact]
    public async Task CreateTrack_ShouldCreateTrack()
    {
        var artistId = Guid.NewGuid();
        var artist = new Artist()
        {
            Id = artistId,
            Name = "TestArtist",
            Grammy = false,
            UpdatedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false
        };
        Context.Artists.Add(artist);
        await Context.SaveChangesAsync();

        var albumId = Guid.NewGuid();
        var album = new Album
        {
            Id = albumId,
            Name = "Album",
            Year = 2025,
            ArtistId = artistId,
            UpdatedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false
        };
        Context.Albums.Add(album);
        await Context.SaveChangesAsync();

        var trackId = Guid.NewGuid();
        var track = new Track
        {
            Id = trackId,
            Name = "Track",
            AlbumId = albumId,
            Duration = 132,
            UpdatedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false
        };
        Context.Tracks.Add(track);
        await Context.SaveChangesAsync();

        var createdTrack = await Context.Tracks.FindAsync(trackId);
        Assert.NotNull(createdTrack);
        Assert.Equal("Track", createdTrack.Name);
        Assert.Equal(trackId, createdTrack.Id);
        Assert.Equal(albumId, createdTrack.AlbumId);
        Assert.Equal(132, createdTrack.Duration);
    }

    [Fact]
    public async Task DeleteTrack_ShouldDeleteTrack()
    {
        var artistId = Guid.NewGuid();
        var artist = new Artist()
        {
            Id = artistId,
            Name = "TestArtist",
            Grammy = false,
            UpdatedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false
        };
        Context.Artists.Add(artist);
        await Context.SaveChangesAsync();

        var albumId = Guid.NewGuid();
        var album = new Album
        {
            Id = albumId,
            Name = "Album",
            Year = 2025,
            ArtistId = artistId,
            UpdatedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false
        };
        Context.Albums.Add(album);
        await Context.SaveChangesAsync();

        var trackId = Guid.NewGuid();
        var track = new Track
        {
            Id = trackId,
            Name = "Track",
            AlbumId = albumId,
            Duration = 132,
            UpdatedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false
        };
        Context.Tracks.Add(track);
        await Context.SaveChangesAsync();

        var createdTrack = await Context.Tracks.FindAsync(trackId);
        Context.Tracks.Remove(createdTrack);
        await Context.SaveChangesAsync();

        var deletedTrack = await Context.Tracks.FindAsync(trackId);
        Assert.Null(deletedTrack);
    }

    [Fact]
    public async Task UpdateTrack_ShouldUpdateTrack()
    {
        var artistId = Guid.NewGuid();
        var artist = new Artist()
        {
            Id = artistId,
            Name = "TestArtist",
            Grammy = false,
            UpdatedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false
        };
        Context.Artists.Add(artist);
        await Context.SaveChangesAsync();

        var albumId = Guid.NewGuid();
        var album = new Album
        {
            Id = albumId,
            Name = "Album",
            Year = 2025,
            ArtistId = artistId,
            UpdatedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false
        };
        Context.Albums.Add(album);
        await Context.SaveChangesAsync();

        var trackId = Guid.NewGuid();
        var track = new Track
        {
            Id = trackId,
            Name = "Track",
            AlbumId = albumId,
            Duration = 132,
            UpdatedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false
        };
        Context.Tracks.Add(track);
        await Context.SaveChangesAsync();

        var createdTrack = await Context.Tracks.FindAsync(trackId);
        createdTrack.Name = "UpdatedTrack";
        await Context.SaveChangesAsync();

        var updatedTrack = await Context.Tracks.FirstOrDefaultAsync(x => x.Id == trackId);

        Assert.NotNull(updatedTrack);
        Assert.Equal("UpdatedTrack", updatedTrack.Name);
    }

    [Fact]
    public async Task SoftDeleteTrack_ShouldSoftDeleteTrack()
    {
        var artistId = Guid.NewGuid();
        var artist = new Artist()
        {
            Id = artistId,
            Name = "TestArtist",
            Grammy = false,
            UpdatedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false
        };
        Context.Artists.Add(artist);
        await Context.SaveChangesAsync();

        var albumId = Guid.NewGuid();
        var album = new Album
        {
            Id = albumId,
            Name = "Album",
            Year = 2025,
            ArtistId = artistId,
            UpdatedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false
        };
        Context.Albums.Add(album);
        await Context.SaveChangesAsync();

        var trackId = Guid.NewGuid();
        var track = new Track
        {
            Id = trackId,
            Name = "Track",
            AlbumId = albumId,
            Duration = 132,
            UpdatedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false
        };
        Context.Tracks.Add(track);
        await Context.SaveChangesAsync();

        var createdTrack = await Context.Tracks.FindAsync(trackId);
        Assert.NotNull(createdTrack);
        createdTrack.IsDeleted = true;
        await Context.SaveChangesAsync();

        Assert.NotNull(createdTrack);
        Assert.Equal("Track", createdTrack.Name);
        Assert.Equal(trackId, createdTrack.Id);
        Assert.True(createdTrack.IsDeleted);
    }

    [Fact]
    public async Task GetTrack_ShouldLoadArtistAndAlbum()
    {
        var artistId = Guid.NewGuid();
        var artist = new Artist()
        {
            Id = artistId,
            Name = "TestArtist",
            Grammy = false,
            UpdatedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false
        };
        Context.Artists.Add(artist);
        await Context.SaveChangesAsync();

        var albumId = Guid.NewGuid();
        var album = new Album
        {
            Id = albumId,
            Name = "Album",
            Year = 2025,
            ArtistId = artistId,
            UpdatedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false
        };
        Context.Albums.Add(album);
        await Context.SaveChangesAsync();

        var trackId = Guid.NewGuid();
        var track = new Track
        {
            Id = trackId,
            Name = "Track",
            AlbumId = albumId,
            Duration = 132,
            UpdatedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false
        };

        var trackArtists = new TracksArtists()
        {
            TrackId = trackId,
            ArtistId = artistId,
            UpdatedAt = DateTime.UtcNow,
            IsDeleted = false
        };
        Context.Tracks.Add(track);
        Context.TracksArtists.Add(trackArtists);
        await Context.SaveChangesAsync();

        var trackWithRelations = await Context.Tracks
            .Include(t => t.TrackArtists)
            .ThenInclude(tracksArtists => tracksArtists.Artist)
            .Include(t => t.Album)
            .FirstOrDefaultAsync(x => x.Id == trackId && x.IsDeleted == false);

        var primaryArtist = trackWithRelations?.TrackArtists.FirstOrDefault()?.Artist;

        Assert.NotNull(primaryArtist);
        Assert.Equal("TestArtist", primaryArtist.Name);
        Assert.NotNull(trackWithRelations?.Album);
        Assert.Equal("Album", trackWithRelations.Album.Name);
    }


    [Fact]
    public async Task AddTrack_ShouldAddTrack()
    {
        var artistId = Guid.NewGuid();
        var artist = new Artist()
        {
            Id = artistId,
            Name = "TestArtist",
            Grammy = false,
            UpdatedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false
        };
        Context.Artists.Add(artist);
        await Context.SaveChangesAsync();

        var albumId = Guid.NewGuid();
        var album = new Album
        {
            Id = albumId,
            Name = "Album",
            Year = 2025,
            ArtistId = artistId,
            UpdatedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false
        };
        Context.Albums.Add(album);
        await Context.SaveChangesAsync();

        var trackId = Guid.NewGuid();
        var track = new Track
        {
            Id = trackId,
            Name = "Track",
            AlbumId = albumId,
            Duration = 132,
            UpdatedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false
        };
        Context.Tracks.Add(track);
        await Context.SaveChangesAsync();

        var createdTrack = await Context.Tracks.FindAsync(trackId);
        Assert.NotNull(createdTrack);
        Assert.Equal("Track", createdTrack.Name);
        Assert.Equal(trackId, createdTrack.Id);
        Assert.Equal(albumId, createdTrack.AlbumId);
        Assert.Equal(132, createdTrack.Duration);
    }
}