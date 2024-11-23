using identity_service.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<SocialLink> SocialLinks { get; set; }
    public DbSet<Role> Roles { get; set; }
}