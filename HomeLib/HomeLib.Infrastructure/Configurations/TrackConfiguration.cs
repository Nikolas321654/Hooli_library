using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HomeLib.Infrastructure.Model;

namespace HomeLib.Infrastructure.Configurations;

public class TrackConfiguration : IEntityTypeConfiguration<TrackEntity>
{
    public void Configure(EntityTypeBuilder<TrackEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasDefaultValueSql("gen_random_uuid()");

        builder.HasOne(t => t.Artist)
            .WithMany(artist => artist.Tracks)
            .HasForeignKey(t => t.ArtistId);

        builder.HasOne(t => t.Album)
            .WithMany(album => album.Tracks)
            .HasForeignKey(t => t.AlbumId);
        
        builder.HasMany(t => t.PlaylistTrack)
            .WithOne(p => p.Track)
            .HasForeignKey(t => t.TrackId);
    }
}