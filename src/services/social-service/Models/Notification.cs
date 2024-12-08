namespace creator_service.Models;

public class Notification
{
    public int Id { get; set; }
    public Guid  UserId { get; set; }
    public string Message { get; set; }
    public bool IsRead { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string NotificationType { get; set; }
}