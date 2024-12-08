using creator_service.Dtos.notifications;
using creator_service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace creator_service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationsController : ControllerBase
{
    private readonly INotificationService _notificationService;

    public NotificationsController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<IEnumerable<NotificationDto>>> GetNotificationsByUserId(Guid  userId)
    {
        var notifications = await _notificationService.GetNotificationsByUserAsync(userId);
        return Ok(notifications);
    }

    [HttpPost]
    public async Task<ActionResult<NotificationDto>> CreateNotification([FromBody] CreateNotificationDto dto)
    {
        var notification = await _notificationService.CreateNotificationAsync(dto);
        return CreatedAtAction(nameof(GetNotificationsByUserId), new { userId = notification.UserId }, notification);
    }
}