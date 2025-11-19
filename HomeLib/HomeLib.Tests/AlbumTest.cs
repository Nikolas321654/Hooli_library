using HomeLib.Core.Model;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace HomeLib.Tests;

public class AlbumTest : IntegrationTestBase
{
    [Fact]
    public async Task CreateAlbum_ShouldCreateAlbum()
    {
        Guid artistId = Guid.NewGuid();
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
        
        Guid id = Guid.NewGuid();
        var album = new Album
        {
            Id = id,
            Name = "Album",
            Year = 2025,
            ArtistId = artistId,
            UpdatedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false
        };

        Context.Albums.Add(album);
        await Context.SaveChangesAsync();

        var createdAlbum = await Context.Albums.FindAsync(album.Id);
        Assert.NotNull(createdAlbum);
        Assert.Equal(id, createdAlbum.Id);
        Assert.Equal("Album", createdAlbum.Name);
        Assert.Equal(2025, createdAlbum.Year);
        Assert.Equal(artistId, createdAlbum.ArtistId);
        Assert.False(createdAlbum.IsDeleted);
    }

    [Fact]
    public async Task DeleteAlbum_ShouldDeleteAlbum()
    {
        Guid artistId = Guid.NewGuid();
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

        Guid id = Guid.NewGuid();
        var album = new Album
        {
            Id = id,
            Name = "Album",
            ArtistId = artistId,
            Year = 2025,
            UpdatedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false
        };
        Context.Albums.Add(album);
        await Context.SaveChangesAsync();

        var albumToDelete = await Context.Albums.FindAsync(id);
        Context.Remove(albumToDelete);
        await Context.SaveChangesAsync();

        var deletedAlbum = await Context.Albums.FindAsync(id);
        Assert.Null(deletedAlbum);
    }

    [Fact]
    public async Task UpdateAlbum_ShouldUpdateAlbum()
    {
        Guid artistId = Guid.NewGuid();
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

        Guid albumId = Guid.NewGuid();
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

        var albumToUpdate = await Context.Albums.FindAsync(albumId);
        album.Name = "Updated Album";
        album.UpdatedAt = DateTime.UtcNow;
        await Context.SaveChangesAsync();

        var updatedAlbum = await Context.Albums.FindAsync(albumId);
        Assert.NotNull(updatedAlbum);
        Assert.Equal("Updated Album", updatedAlbum.Name);
        Assert.Equal(2025, updatedAlbum.Year);
    }
    
    [Fact]
    public async Task SoftDeleteAlbum_ShouldSoftDeleteAlbum()
    {
        Guid artistId = Guid.NewGuid();
        
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

        Guid id = Guid.NewGuid();
        var album = new Album
        {
            Id = id,
            Name = "Album",
            ArtistId = artistId,
            Year = 2025,
            UpdatedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false
        };
        Context.Albums.Add(album);
        await Context.SaveChangesAsync();

        var albumToModify = await Context.Albums.FindAsync(id); 
        Assert.NotNull(albumToModify);
        albumToModify.IsDeleted = true;
        albumToModify.UpdatedAt = DateTime.UtcNow;
        await Context.SaveChangesAsync();

        var finalAlbum = await Context.Albums.FindAsync(id);
        Assert.NotNull(finalAlbum);
        Assert.True(finalAlbum.IsDeleted);
    }
    
    [Fact]
    public async Task AddTrack_ShouldAddTrack()
    {
        Guid artistId = Guid.NewGuid();
        
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

        Guid albumId = Guid.NewGuid();
        var album = new Album
        {
            Id = albumId,
            Name = "Album",
            ArtistId = artistId,
            Year = 2025,
            UpdatedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false
        };
        Context.Albums.Add(album);
        await Context.SaveChangesAsync();

        Guid trackId = Guid.NewGuid();
        var track = new Track
        {
            Id = trackId,
            Name = "Track",
            AlbumId = album.Id,
            Duration = 132,
            UpdatedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false
        };
        Context.Tracks.Add(track);
        await Context.SaveChangesAsync();
        
        var trackInAlbum = await Context.Tracks.FindAsync(trackId);
        Assert.NotNull(trackInAlbum);
        
        Assert.Equal(track.Name, trackInAlbum.Name);
        Assert.Equal(track.AlbumId, trackInAlbum.AlbumId);
    }
}