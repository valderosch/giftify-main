using payment_service.Dtos;

namespace payment_service.Interfaces;

public interface ITransactionService
{
    Task<(bool IsSuccess, string ErrorMessage, TransactionDto Transaction)> CreateTransactionAsync(CreateTransactionDto dto);
    Task<TransactionDto> GetTransactionByIdAsync(Guid id);
    Task<IEnumerable<TransactionDto>> GetTransactionsByUserIdAsync(Guid userId);
    Task<bool> DeleteTransactionAsync(Guid id);
}