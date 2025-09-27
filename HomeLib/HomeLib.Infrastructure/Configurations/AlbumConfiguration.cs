using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HomeLib.Infrastructure.Model;

namespace HomeLib.Infrastructure.Configurations;

public class AlbumConfiguration : IEntityTypeConfiguration<AlbumEntity>
{
    public void Configure(EntityTypeBuilder<AlbumEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasDefaultValueSql("gen_random_uuid()");
        
        builder.HasOne(a => a.Artist)
            .WithMany(artist => artist.Albums)
            .HasForeignKey(a => a.ArtistId);

        builder.HasMany(a => a.Tracks)
            .WithOne(track => track.Album)
            .HasForeignKey(track => track.AlbumId);
    }
}