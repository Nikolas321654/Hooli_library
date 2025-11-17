using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HomeLib.Core;
using HomeLib.Core.Model;

namespace HomeLib.Infrastructure.Configurations;

public class TracksArtistsConfiguration : IEntityTypeConfiguration<TracksArtists>
{
    public void Configure(EntityTypeBuilder<TracksArtists> builder)
    {
        builder.HasKey(ta => new { ta.ArtistId, ta.TrackId });

        builder.HasOne(ta => ta.Track)
            .WithMany(t => t.TrackArtists)
            .HasForeignKey(ta => ta.TrackId);

        builder.HasOne(ta => ta.Artist)
            .WithMany(a => a.TrackArtists)
            .HasForeignKey(ta => ta.ArtistId);

        builder.HasIndex(ta => ta.TrackId);
        builder.HasIndex(ta => ta.ArtistId);
    }
}