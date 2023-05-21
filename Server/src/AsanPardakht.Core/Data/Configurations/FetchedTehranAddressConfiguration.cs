using AsanPardakht.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AsanPardakht.Core.Data.Configurations;

public class FetchedTehranAddressConfiguration : IEntityTypeConfiguration<FetchedTehranAddress>
{
    public void Configure(EntityTypeBuilder<FetchedTehranAddress> builder)
    {
        builder.ToTable("FetchedTehranAddresses");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Address)
            .IsRequired();

        builder.Property(x => x.OriginalCreatedAt);
    }
}