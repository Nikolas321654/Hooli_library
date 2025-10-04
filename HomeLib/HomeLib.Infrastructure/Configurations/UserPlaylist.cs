using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HomeLib.Core;

namespace HomeLib.Infrastructure.Configurations;

public class UserPlaylistConfiguration : IEntityTypeConfiguration<UserPlaylists>
{
    public void Configure(EntityTypeBuilder<UserPlaylists> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasDefaultValueSql("gen_random_uuid()");

        builder.HasMany(a => a.PlaylistTracks)
            .WithOne(x => x.UserPlaylists)
            .HasForeignKey(a => a.PlaylistId);
    }
}