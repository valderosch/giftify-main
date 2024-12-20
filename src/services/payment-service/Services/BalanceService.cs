using payment_service.Dtos;
using payment_service.Interfaces;
using payment_service.Models;

namespace payment_service.Services;

public class BalanceService : IBalanceService
{
    private readonly IBalanceRepository _balanceRepository;

    public BalanceService(IBalanceRepository balanceRepository)
    {
        _balanceRepository = balanceRepository;
    }

    public async Task<(bool IsSuccess, string ErrorMessage, decimal Balance)> GetBalanceAsync(Guid userId)
    {
        var balance = await _balanceRepository.GetBalanceByUserIdAsync(userId);
        if (balance != null)
            return (true, null, balance.Amount);

        return (false, "Balance not found for the specified user.", 0);
    }

    public async Task<(bool IsSuccess, string ErrorMessage, decimal Balance)> TopUpBalanceAsync(TopUpBalanceDto dto)
    {
        if (dto.Amount <= 0)
            return (false, "Amount must be greater than zero.", 0);

        var balance = await _balanceRepository.GetBalanceByUserIdAsync(dto.UserId);

        if (balance == null)
        {
            balance = new Balance { UserId = dto.UserId, Amount = dto.Amount };
            await _balanceRepository.AddAsync(balance);
        }
        else
        {
            balance.Amount += dto.Amount;
            await _balanceRepository.UpdateAsync(balance);
        }

        return (true, null, balance.Amount);
    }

    public async Task<(bool IsSuccess, string ErrorMessage, decimal Balance)> WithdrawBalanceAsync(WithdrawBalanceDto dto)
    {
        if (dto.Amount <= 0)
            return (false, "Amount must be greater than zero.", 0);

        var balance = await _balanceRepository.GetBalanceByUserIdAsync(dto.UserId);
        if (balance == null || balance.Amount < dto.Amount)
            return (false, "Insufficient balance.", 0);

        balance.Amount -= dto.Amount;
        await _balanceRepository.UpdateAsync(balance);

        return (true, null, balance.Amount);
    }
}