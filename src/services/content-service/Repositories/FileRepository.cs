using content_service.Data;
using content_service.Models;
using Microsoft.EntityFrameworkCore;

namespace content_service.Repositories;

public class FileRepository
{
    private readonly AppDbContext _context;

    public FileRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<FileAttachment?> GetFileByIdAsync(int id)
    {
        return await _context.FileAttachments.FindAsync(id);
    }

    public async Task AddFileAsync(FileAttachment file)
    {
        await _context.FileAttachments.AddAsync(file);
        await _context.SaveChangesAsync();
    }

    public async Task<List<FileAttachment>> GetFilesByPostIdAsync(int postId)
    {
        return await _context.FileAttachments.Where(f => f.PostId == postId).ToListAsync();
    }
}