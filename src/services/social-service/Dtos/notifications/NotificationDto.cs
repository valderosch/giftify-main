namespace creator_service.Dtos.notifications;

public class NotificationDto
{
    public int Id { get; set; }
    public Guid  UserId { get; set; }
    public string Message { get; set; }
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; }
    public string NotificationType { get; set; }
}