using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using  HomeLib.Core;
using HomeLib.Core.Model;

namespace HomeLib.Infrastructure.Configurations;

public class AlbumConfiguration : IEntityTypeConfiguration<Album>
{
    public void Configure(EntityTypeBuilder<Album> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasOne(a => a.Artist)
            .WithMany(artist => artist.Albums)
            .HasForeignKey(a => a.ArtistId);

        builder.HasMany(a => a.Tracks)
            .WithOne(track => track.Album)
            .HasForeignKey(track => track.AlbumId);
    }
}