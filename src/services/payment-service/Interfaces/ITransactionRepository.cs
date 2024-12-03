using payment_service.Models;

namespace payment_service.Interfaces;

public interface ITransactionRepository
{
    Task AddAsync(Transaction transaction);
    Task<Transaction> GetByIdAsync(Guid id);
    Task<IEnumerable<Transaction>> GetByUserIdAsync(Guid userId);
    Task<bool> DeleteAsync(Guid id);
}