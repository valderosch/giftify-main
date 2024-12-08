using creator_service.Models;

namespace creator_service.Interfaces;

public interface INotificationRepository
{
    Task<IEnumerable<Notification>> GetByUserIdAsync(Guid  userId);
    Task<Notification> GetByIdAsync(int id);
    Task AddAsync(Notification notification);
    Task DeleteAsync(Notification notification);
}