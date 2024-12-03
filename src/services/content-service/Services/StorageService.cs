using content_service.Models;
using content_service.Repositories;

namespace content_service.Services;

public class StorageService
{
    private readonly FileRepository _fileRepository;

    public StorageService(FileRepository fileRepository)
    {
        _fileRepository = fileRepository;
    }

    public async Task SaveFileAsync(FileAttachment file)
    {
        await _fileRepository.AddFileAsync(file);
    }

    public async Task<FileAttachment?> GetFileByIdAsync(int id)
    {
        return await _fileRepository.GetFileByIdAsync(id);
    }
}