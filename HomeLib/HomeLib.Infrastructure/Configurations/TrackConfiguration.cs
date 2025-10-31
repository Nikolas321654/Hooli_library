using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using  HomeLib.Core;
using HomeLib.Core.Model;

namespace HomeLib.Infrastructure.Configurations;

public class TrackConfiguration : IEntityTypeConfiguration<Track>
{
    public void Configure(EntityTypeBuilder<Track> builder)
    {
        builder.HasKey(x => x.Id);

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