using payment_service.Models;

namespace payment_service.Interfaces;

public interface IBalanceRepository
{
    Task<Balance> GetBalanceByUserIdAsync(Guid userId);
    Task AddAsync(Balance balance);
    Task UpdateAsync(Balance balance);
}