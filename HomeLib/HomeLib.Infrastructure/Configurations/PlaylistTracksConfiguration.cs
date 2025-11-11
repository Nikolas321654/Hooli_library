using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using  HomeLib.Core;
namespace HomeLib.Infrastructure.Configurations;

public class PlaylistTrackConfiguration : IEntityTypeConfiguration<PlaylistTracks>
{
    public void Configure(EntityTypeBuilder<PlaylistTracks> builder)
    {
        builder.HasKey(pt => new { pt.TrackId, pt.PlaylistId });
        
        builder.HasOne(pt => pt.Track)
            .WithMany(t => t.PlaylistTrack)
            .HasForeignKey(pt => pt.TrackId);

        builder.HasOne(pt => pt.UserPlaylists)
            .WithMany(up => up.PlaylistTracks)
            .HasForeignKey(pt => pt.PlaylistId);
    }
}