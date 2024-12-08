using creator_service.Dtos.notifications;

namespace creator_service.Interfaces;

public interface INotificationService
{
    Task<IEnumerable<NotificationDto>> GetNotificationsByUserAsync(Guid  userId);
    Task<NotificationDto> GetNotificationByIdAsync(int id);
    Task<NotificationDto> CreateNotificationAsync(CreateNotificationDto dto);
    Task<bool> DeleteNotificationAsync(int id);
}