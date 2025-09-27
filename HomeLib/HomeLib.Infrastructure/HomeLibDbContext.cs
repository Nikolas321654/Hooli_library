using HomeLib.Infrastructure.Model;
using HomeLib.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace HomeLib.Infrastructure;

public class HomeLibDbContext : DbContext
{
    public HomeLibDbContext(DbContextOptions<HomeLibDbContext> options) : base(options)
    {
    }

    public DbSet<AlbumEntity> Albums { get; set; }
    public DbSet<ArtistEntity> Artists { get; set; }
    public DbSet<TrackEntity> Tracks { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<UserPlaylistsEntity> UserPlaylists { get; set; }
    public DbSet<PlaylistTracksEntity> PlaylistTracks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TrackConfiguration());
        modelBuilder.ApplyConfiguration(new ArtistConfiguration());
        modelBuilder.ApplyConfiguration(new AlbumConfiguration());
        modelBuilder.ApplyConfiguration(new UserPlaylistConfiguration());
        modelBuilder.ApplyConfiguration(new PlaylistTrackConfiguration());
    }
}