using content_service.Models;
using Microsoft.EntityFrameworkCore;

namespace content_service.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Post> Posts { get; set; }
    public DbSet<Goal> Goals { get; set; }
    public DbSet<FileAttachment> FileAttachments { get; set; }
    public DbSet<UserSubscription> UserSubscriptions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>().HasMany(p => p.FileAttachments).WithOne(f => f.Post).HasForeignKey(f => f.PostId);
        base.OnModelCreating(modelBuilder);
    }
}