using creator_service.Data;
using creator_service.Interfaces;
using creator_service.Models;
using Microsoft.EntityFrameworkCore;

namespace creator_service.Repository;

public class NotificationRepository : INotificationRepository
{
    private readonly AppDbContext _context;

    public NotificationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Notification>> GetByUserIdAsync(Guid  userId)
    {
        return await _context.Notifications
            .Where(n => n.UserId == userId)
            .ToListAsync();
    }

    public async Task<Notification> GetByIdAsync(int id)
    {
        return await _context.Notifications.FindAsync(id);
    }

    public async Task AddAsync(Notification notification)
    {
        await _context.Notifications.AddAsync(notification);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Notification notification)
    {
        _context.Notifications.Remove(notification);
        await _context.SaveChangesAsync();
    }
}