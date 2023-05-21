using AsanPardakht.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AsanPardakht.Core.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Username).HasMaxLength(32)
            .HasColumnType("NVARCHAR(250)")
            .IsRequired();

        builder.HasMany(x => x.Locations)
            .WithOne()
            .HasForeignKey(x => x.UserId)
            .HasPrincipalKey(x => x.Id)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}