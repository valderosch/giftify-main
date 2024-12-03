using Microsoft.EntityFrameworkCore;
using payment_service.Interfaces;
using payment_service.Models;

namespace payment_service.Repositories;

public class BalanceRepository : IBalanceRepository
{
    private readonly AppDbContext _context;

    public BalanceRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Balance> GetBalanceByUserIdAsync(Guid userId)
    {
        return await _context.Balances.FirstOrDefaultAsync(b => b.UserId == userId);
    }

    public async Task AddAsync(Balance balance)
    {
        _context.Balances.Add(balance);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Balance balance)
    {
        _context.Balances.Update(balance);
        await _context.SaveChangesAsync();
    }
}