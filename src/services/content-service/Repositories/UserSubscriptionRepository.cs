using content_service.Data;
using content_service.Models;
using Microsoft.EntityFrameworkCore;

namespace content_service.Repositories;

public class UserSubscriptionRepository
{
    private readonly AppDbContext _context;

    public UserSubscriptionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Guid>> GetSubscribedAuthorIdsAsync(Guid userId)
    {
        return await _context.UserSubscriptions
            .Where(us => us.UserId == userId)
            .Select(us => us.AuthorId)
            .ToListAsync();
    }

    public async Task<bool> IsSubscribedAsync(Guid userId, Guid authorId)
    {
        return await _context.UserSubscriptions
            .AnyAsync(us => us.UserId == userId && us.AuthorId == authorId);
    }

    public async Task AddSubscriptionAsync(UserSubscription subscription)
    {
        await _context.UserSubscriptions.AddAsync(subscription);
        await _context.SaveChangesAsync();
    }
}