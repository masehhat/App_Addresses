using AsanPardakht.Core.Data.Configurations;
using AsanPardakht.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AsanPardakht.Core.Data;

public class AsanPardakhtDbContext : DbContext
{
    public AsanPardakhtDbContext(DbContextOptions<AsanPardakhtDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Location> Locations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}