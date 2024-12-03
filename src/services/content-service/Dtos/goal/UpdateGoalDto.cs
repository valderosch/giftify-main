namespace content_service.Dtos.goal;

public class UpdateGoalDto
{
    public string? Title { get; set; }
    public decimal? TargetAmount { get; set; }
    public string? Description { get; set; }
    public DateTime? Deadline { get; set; }
}