using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HomeLib.Infrastructure.Model;

namespace HomeLib.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasDefaultValueSql("gen_random_uuid()");
        
        builder.HasMany(u => u.UserPlaylist)
            .WithOne(up => up.User)
            .HasForeignKey(up => up.UserId);
    }
}