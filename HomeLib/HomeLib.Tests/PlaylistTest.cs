using HomeLib.Core;
using HomeLib.Core.Model;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace HomeLib.Tests;

public class PlaylistTest : IntegrationTestBase
{
    [Fact]
    public async Task CreatePlaylist_ShouldCreateAPlaylist()
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

        var userId = Guid.NewGuid();
        var user = new User
        {
            Id = userId,
            Name = "User",
            Password = "password",
            Login = "login",
            Version = 1,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        Context.Users.Add(user);
        await Context.SaveChangesAsync();

        var playlistId = Guid.NewGuid();
        var playlist = new UserPlaylists()
        {
            Id = playlistId,
            Name = "Playlist",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            UserId = userId
        };
        Context.UserPlaylists.Add(playlist);
        await Context.SaveChangesAsync();

        var trackPlaylist = new PlaylistTracks()
        {
            TrackId = trackId,
            PlaylistId = playlistId,
            Position = 0,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        Context.PlaylistTracks.Add(trackPlaylist);
        await Context.SaveChangesAsync();

        var userPlaylist = Context.UserPlaylists.Where(x => x.UserId == userId);
        Assert.NotNull(userPlaylist);
    }

    [Fact]
    public async Task GetPlaylistTrack_ShouldGetPlaylistTrack()
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

        var userId = Guid.NewGuid();
        var user = new User
        {
            Id = userId,
            Name = "User",
            Password = "password",
            Login = "login",
            Version = 1,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        Context.Users.Add(user);
        await Context.SaveChangesAsync();

        var playlistId = Guid.NewGuid();
        var playlist = new UserPlaylists()
        {
            Id = playlistId,
            Name = "Playlist",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            UserId = userId
        };
        Context.UserPlaylists.Add(playlist);
        await Context.SaveChangesAsync();

        var trackPlaylist = new PlaylistTracks()
        {
            TrackId = trackId,
            PlaylistId = playlistId,
            Position = 0,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        Context.PlaylistTracks.Add(trackPlaylist);
        await Context.SaveChangesAsync();

        var userPlaylist = Context.UserPlaylists
            .Where(x => x.UserId == userId)
            .Include(x => x.PlaylistTracks)
            .ThenInclude(x => x.Track)
            .FirstOrDefault();

        Assert.NotNull(userPlaylist);
        var playlistTrack = userPlaylist.PlaylistTracks.FirstOrDefault(x => x.TrackId == trackId);
        Assert.NotNull(playlistTrack);
        Assert.Equal("Track", playlistTrack.Track.Name);
    }

    [Fact]
    public async Task DeleteTrackPlaylist_ShouldDeleteTrackPlaylist()
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

        var userId = Guid.NewGuid();
        var user = new User
        {
            Id = userId,
            Name = "User",
            Password = "password",
            Login = "login",
            Version = 1,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        Context.Users.Add(user);
        await Context.SaveChangesAsync();

        var playlistId = Guid.NewGuid();
        var playlist = new UserPlaylists()
        {
            Id = playlistId,
            Name = "Playlist",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            UserId = userId
        };
        Context.UserPlaylists.Add(playlist);
        await Context.SaveChangesAsync();

        var trackPlaylist = new PlaylistTracks()
        {
            TrackId = trackId,
            PlaylistId = playlistId,
            Position = 0,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        Context.PlaylistTracks.Add(trackPlaylist);
        await Context.SaveChangesAsync();

        var playlistTrack = await Context.PlaylistTracks
            .Where(x =>
                x.PlaylistId == playlistId
                && x.TrackId == trackId
                && x.UserPlaylists.UserId == userId)
            .FirstOrDefaultAsync();

        Context.PlaylistTracks.Remove(playlistTrack);
        await Context.SaveChangesAsync();

        var deletedTrack = await Context.PlaylistTracks
            .Where(x =>
                x.PlaylistId == playlistId
                && x.TrackId == trackId
                && x.UserPlaylists.UserId == userId)
            .FirstOrDefaultAsync();
        Assert.Null(deletedTrack);
    }

    [Fact]
    public async Task DeletePlaylist_ShouldDeletePlaylist()
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

        var userId = Guid.NewGuid();
        var user = new User
        {
            Id = userId,
            Name = "User",
            Password = "password",
            Login = "login",
            Version = 1,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        Context.Users.Add(user);
        await Context.SaveChangesAsync();

        var userPlaylistId = Guid.NewGuid();
        var userPlaylist = new UserPlaylists()
        {
            Id = userPlaylistId,
            Name = "Playlist",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            UserId = userId
        };
        Context.UserPlaylists.Add(userPlaylist);
        await Context.SaveChangesAsync();

        var trackPlaylist = new PlaylistTracks()
        {
            TrackId = trackId,
            PlaylistId = userPlaylistId,
            Position = 0,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        Context.PlaylistTracks.Add(trackPlaylist);
        await Context.SaveChangesAsync();

        var playlist = await Context.UserPlaylists
            .Where(x => x.UserId == userId && x.Id == userPlaylistId)
            .Include(t => t.PlaylistTracks)
            .ThenInclude(pt => pt.Track)
            .FirstOrDefaultAsync();

        Assert.NotNull(playlist);
        Context.Remove(playlist);
        await Context.SaveChangesAsync();

        var deletedPlaylist = await Context.UserPlaylists.FindAsync(userPlaylistId);
        Assert.Null(deletedPlaylist);
    }

    [Fact]
    public async Task UpdatePlaylist_ShouldUpdatePlaylist()
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

        var userId = Guid.NewGuid();
        var user = new User
        {
            Id = userId,
            Name = "User",
            Password = "password",
            Login = "login",
            Version = 1,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        Context.Users.Add(user);
        await Context.SaveChangesAsync();

        var userPlaylistId = Guid.NewGuid();
        var userPlaylist = new UserPlaylists()
        {
            Id = userPlaylistId,
            Name = "Playlist",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            UserId = userId
        };
        Context.UserPlaylists.Add(userPlaylist);
        await Context.SaveChangesAsync();

        var trackPlaylist = new PlaylistTracks()
        {
            TrackId = trackId,
            PlaylistId = userPlaylistId,
            Position = 0,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        Context.PlaylistTracks.Add(trackPlaylist);
        await Context.SaveChangesAsync();

        var playlist = await Context.UserPlaylists
            .Where(x => x.UserId == userId && x.Id == userPlaylistId)
            .Include(t => t.PlaylistTracks)
            .ThenInclude(pt => pt.Track)
            .FirstOrDefaultAsync();

        playlist.Name = "PlaylistNew";
        playlist.UpdatedAt = DateTime.UtcNow;
        await Context.SaveChangesAsync();

        var updatedPlaylist = await Context.UserPlaylists.FindAsync(userPlaylistId);
        Assert.NotNull(updatedPlaylist);
        Assert.Equal("PlaylistNew", updatedPlaylist.Name);
    }
}