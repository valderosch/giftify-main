using creator_service.Models;

namespace creator_service.Interfaces;

public interface ICollectionRepository
{
    Task<IEnumerable<Collection>> GetByUserIdAsync(Guid  userId);
    Task<Collection> GetByIdAsync(int id);
    Task AddAsync(Collection collection);
    Task UpdateAsync(Collection collection);
    Task DeleteAsync(Collection collection);
}