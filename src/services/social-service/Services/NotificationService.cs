using creator_service.Dtos.notifications;
using creator_service.Interfaces;
using creator_service.Models;

namespace creator_service.Services;

public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<IEnumerable<NotificationDto>> GetNotificationsByUserAsync(Guid  userId)
        {
            var notifications = await _notificationRepository.GetByUserIdAsync(userId);
            return notifications.Select(n => new NotificationDto
            {
                Id = n.Id,
                UserId = n.UserId,
                Message = n.Message,
                NotificationType = n.NotificationType,
                CreatedAt = n.CreatedAt
            });
        }

        public async Task<NotificationDto> GetNotificationByIdAsync(int id)
        {
            var notification = await _notificationRepository.GetByIdAsync(id);
            if (notification == null) return null;

            return new NotificationDto
            {
                Id = notification.Id,
                UserId = notification.UserId,
                Message = notification.Message,
                NotificationType = notification.NotificationType,
                CreatedAt = notification.CreatedAt
            };
        }

        public async Task<NotificationDto> CreateNotificationAsync(CreateNotificationDto dto)
        {
            var notification = new Notification
            {
                UserId = dto.UserId,
                Message = dto.Message,
                NotificationType = dto.NotificationType,
                CreatedAt = DateTime.UtcNow
            };

            await _notificationRepository.AddAsync(notification);
            return new NotificationDto
            {
                Id = notification.Id,
                UserId = notification.UserId,
                Message = notification.Message,
                NotificationType = notification.NotificationType,
                CreatedAt = notification.CreatedAt
            };
        }

        public async Task<bool> DeleteNotificationAsync(int id)
        {
            var notification = await _notificationRepository.GetByIdAsync(id);
            if (notification == null) return false;

            await _notificationRepository.DeleteAsync(notification);
            return true;
        }
    }