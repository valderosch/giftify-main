using Microsoft.EntityFrameworkCore;
using payment_service.Interfaces;
using payment_service.Models;

namespace payment_service.Repositories;

public class SubscriptionRepository : ISubscriptionRepository
{
    private readonly AppDbContext _context;

    public SubscriptionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Subscription subscription)
    {
        subscription.Status ??= "Active";
        await _context.Subscriptions.AddAsync(subscription);
        await _context.SaveChangesAsync();
    }

    public async Task<Subscription> GetByIdAsync(Guid id)
    {
        return await _context.Subscriptions.FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task UpdateAsync(Subscription subscription)
    {
        _context.Subscriptions.Update(subscription);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Subscription subscription)
    {
        _context.Subscriptions.Remove(subscription);
        await _context.SaveChangesAsync();
    }
}