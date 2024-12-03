using payment_service.Dtos;

namespace payment_service.Interfaces;

public interface IBalanceService
{
    Task<(bool IsSuccess, string ErrorMessage, decimal Balance)> GetBalanceAsync(Guid userId);
    Task<(bool IsSuccess, string ErrorMessage, string Message)> TopUpBalanceAsync(TopUpBalanceDto dto);
    Task<(bool IsSuccess, string ErrorMessage, string Message)> WithdrawBalanceAsync(WithdrawBalanceDto dto);
}