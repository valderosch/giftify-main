namespace creator_service.Dtos.notifications;

public class CreateNotificationDto
{
    public Guid  UserId { get; set; }
    public string Message { get; set; }
    public string NotificationType { get; set; } 
}