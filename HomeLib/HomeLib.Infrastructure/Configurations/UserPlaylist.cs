using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HomeLib.Infrastructure.Model;

namespace HomeLib.Infrastructure.Configurations;

public class UserPlaylistConfiguration : IEntityTypeConfiguration<UserPlaylistsEntity>
{
    public void Configure(EntityTypeBuilder<UserPlaylistsEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasDefaultValueSql("gen_random_uuid()");

        builder.HasMany(a => a.PlaylistTracks)
            .WithOne(x => x.UserPlaylists)
            .HasForeignKey(a => a.PlaylistId);
    }
}