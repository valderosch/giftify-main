using payment_service.Dtos;

namespace payment_service.Interfaces;

public interface ISubscriptionService
{
    Task<SubscriptionDto> CreateSubscriptionAsync(CreateSubscriptionDto dto);
    Task<SubscriptionDto> GetSubscriptionByIdAsync(Guid id);
    Task<bool> UpdateSubscriptionAsync(Guid id, UpdateSubscriptionDto dto);
    Task<bool> DeleteSubscriptionAsync(Guid id);
}