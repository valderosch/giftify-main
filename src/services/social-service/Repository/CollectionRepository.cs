using creator_service.Data;
using creator_service.Interfaces;
using creator_service.Models;
using Microsoft.EntityFrameworkCore;

namespace creator_service.Repository;

public class CollectionRepository : ICollectionRepository
{
    private readonly AppDbContext _context;

    public CollectionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Collection>> GetByUserIdAsync(Guid  userId)
    {
        return await _context.Collections
            .Where(c => c.UserId == userId)
            .ToListAsync();
    }

    public async Task<Collection> GetByIdAsync(int id)
    {
        return await _context.Collections.FindAsync(id);
    }

    public async Task AddAsync(Collection collection)
    {
        await _context.Collections.AddAsync(collection);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Collection collection)
    {
        _context.Collections.Update(collection);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Collection collection)
    {
        _context.Collections.Remove(collection);
        await _context.SaveChangesAsync();
    }
}