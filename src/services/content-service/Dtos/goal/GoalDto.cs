namespace content_service.Dtos.goal;

public class GoalDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public decimal TargetAmount { get; set; }
    public string Description { get; set; }
    public DateTime Deadline { get; set; }
    public decimal CollectedAmount { get; set; }
    public string? ImagePath { get; set; }
}