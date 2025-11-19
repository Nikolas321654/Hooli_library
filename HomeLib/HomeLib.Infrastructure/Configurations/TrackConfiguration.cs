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
        builder.HasIndex(x => new {x.Name, x.CreatedAt} ).HasFilter("\"IsDeleted\" = false");

        builder.HasOne(t => t.Album)
            .WithMany(a => a.Tracks)
            .HasForeignKey(t => t.AlbumId);
    }
}