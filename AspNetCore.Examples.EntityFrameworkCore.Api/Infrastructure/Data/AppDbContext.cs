using Microsoft.EntityFrameworkCore;

namespace AspNetCore.Examples.EntityFrameworkCore.Api.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : DbContext(options)
{
    public DbSet<Resources.Resource> Resources => Set<Resources.Resource>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new Resources.EntityTypeConfiguration().Configure(modelBuilder.Entity<Resources.Resource>());
    }
}
