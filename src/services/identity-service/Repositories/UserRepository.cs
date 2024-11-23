using identity_service.Models;
using IdentityService.Data;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Repositories;

public class UserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUserByEmailAsync(string email) => 
        await _context.Users.Include(u => u.Roles).FirstOrDefaultAsync(u => u.Email == email);

    public async Task AddUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
}