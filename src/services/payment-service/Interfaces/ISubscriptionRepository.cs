using payment_service.Models;

namespace payment_service.Interfaces;

public interface ISubscriptionRepository
{
    Task AddAsync(Subscription subscription);
    Task<Subscription> GetByIdAsync(Guid id);
    Task UpdateAsync(Subscription subscription);
    Task DeleteAsync(Subscription subscription);
}