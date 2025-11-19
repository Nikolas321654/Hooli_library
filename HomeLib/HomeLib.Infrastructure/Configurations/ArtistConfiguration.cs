using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HomeLib.Core;
using HomeLib.Core.Model;

namespace HomeLib.Infrastructure.Configurations;

public class ArtistConfiguration : IEntityTypeConfiguration<Artist>
{
    public void Configure(EntityTypeBuilder<Artist> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => new {x.Name, x.CreatedAt} ).HasFilter("\"IsDeleted\" = false");

        builder.HasMany(a => a.Albums)
            .WithOne(album => album.Artist)
            .HasForeignKey(album => album.ArtistId);
    }
}