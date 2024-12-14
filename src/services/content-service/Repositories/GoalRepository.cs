using content_service.Data;
using content_service.Models;
using Microsoft.EntityFrameworkCore;

namespace content_service.Repositories;

public class GoalRepository
{
    private readonly AppDbContext _context;

    public GoalRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<List<Goal>> GetAllGoalsAsync()
    {
        return await _context.Goals.ToListAsync();
    }

    public async Task<Goal?> GetGoalByIdAsync(int id)
    {
        return await _context.Goals.FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task<List<Goal>> GetGoalsByAuthorAsync(Guid authorId)
    {
        return await _context.Goals.Where(g => g.AuthorId == authorId).ToListAsync();
    }

    public async Task AddGoalAsync(Goal goal)
    {
        await _context.Goals.AddAsync(goal);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateGoalAsync(Goal goal)
    {
        _context.Goals.Update(goal);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteGoalAsync(Goal goal)
    {
        _context.Goals.Remove(goal);
        await _context.SaveChangesAsync();
    }
}