using creator_service.Models;
using Microsoft.EntityFrameworkCore;

namespace creator_service.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Comment> Comments { get; set; }
    public DbSet<Collection> Collections { get; set; }
    public DbSet<Notification> Notifications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>()
            .HasKey(c => c.Id);

        modelBuilder.Entity<Collection>()
            .HasKey(c => c.Id);

        modelBuilder.Entity<Notification>()
            .HasKey(n => n.Id);

        base.OnModelCreating(modelBuilder);
    }
}