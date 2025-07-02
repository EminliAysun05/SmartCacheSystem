using Microsoft.EntityFrameworkCore;
using SmartCacheProject.Domain.Entities;

namespace SmartCacheProject.Migrations;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
       : base(options)
    {
    }
    public DbSet<Service> Services { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Story> Stories { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}

