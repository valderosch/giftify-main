using System.ComponentModel.DataAnnotations;

namespace content_service.Models;

public class Goal
{
    public int Id { get; set; }
    public string Title { get; set; }
    public decimal TargetAmount { get; set; }
    public decimal CollectedAmount { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime Deadline { get; set; }
}