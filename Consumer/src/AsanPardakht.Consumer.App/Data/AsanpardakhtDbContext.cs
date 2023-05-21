using Microsoft.EntityFrameworkCore;

namespace AsanPardakht.Consumer.App;

public class AsanpardakhtDbContext : DbContext
{
    public AsanpardakhtDbContext(DbContextOptions<AsanpardakhtDbContext> options)
                : base(options) { }

    public DbSet<FetchedTehranAddress> FetchedTehranAddresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new FetchedTehranAddressConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}