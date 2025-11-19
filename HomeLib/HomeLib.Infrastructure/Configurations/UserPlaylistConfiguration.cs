using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HomeLib.Core;
using HomeLib.Core.Model;

namespace HomeLib.Infrastructure.Configurations;

public class UserPlaylistConfiguration : IEntityTypeConfiguration<UserPlaylists>
{
    public void Configure(EntityTypeBuilder<UserPlaylists> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => new { x.Name, x.CreatedAt });


        builder.HasMany(a => a.PlaylistTracks)
            .WithOne(x => x.UserPlaylists)
            .HasForeignKey(a => a.PlaylistId);
    }
}