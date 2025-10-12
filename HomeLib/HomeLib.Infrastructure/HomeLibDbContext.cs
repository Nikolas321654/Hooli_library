using HomeLib.Core;
using HomeLib.Core.Model;
using HomeLib.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace HomeLib.Infrastructure;

public class HomeLibDbContext(DbContextOptions<HomeLibDbContext> options) : DbContext(options)
{
    public DbSet<Album> Albums { get; set; }
    public DbSet<Artist> Artists { get; set; }
    public DbSet<Track> Tracks { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserPlaylists> UserPlaylists { get; set; }
    public DbSet<PlaylistTracks> PlaylistTracks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TrackConfiguration());
        modelBuilder.ApplyConfiguration(new ArtistConfiguration());
        modelBuilder.ApplyConfiguration(new AlbumConfiguration());
        modelBuilder.ApplyConfiguration(new UserPlaylistConfiguration());
        modelBuilder.ApplyConfiguration(new PlaylistTrackConfiguration());
    }
}