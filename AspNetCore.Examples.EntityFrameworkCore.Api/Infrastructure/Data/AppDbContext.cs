using AspNetCore.Examples.EntityFrameworkCore.Api.Resources;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.Examples.EntityFrameworkCore.Api.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Resource> Resources => Set<Resource>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new ResourceEntityConfiguration().Configure(modelBuilder.Entity<Resource>());
    }
}
