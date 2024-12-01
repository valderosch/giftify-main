using mail_service.Models;
using Microsoft.EntityFrameworkCore;

namespace mail_service.Data;

public class MailDbContext : DbContext
{
    public DbSet<EmailTemplate> EmailTemplates { get; set; }
    public DbSet<MailQueue> MailQueue { get; set; }

    public MailDbContext(DbContextOptions<MailDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmailTemplate>().HasKey(e => e.Id);
        modelBuilder.Entity<MailQueue>().HasKey(m => m.Id);
    }
}