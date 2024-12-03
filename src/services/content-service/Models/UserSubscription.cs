using System.ComponentModel.DataAnnotations;

namespace content_service.Models;

public class UserSubscription
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public Guid AuthorId { get; set; }
    public DateTime SubscribedAt { get; set; } = DateTime.UtcNow;
}