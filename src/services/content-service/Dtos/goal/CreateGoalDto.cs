namespace content_service.Dtos.goal;

public class CreateGoalDto
{
    public string Title { get; set; }
    public decimal TargetAmount { get; set; }
    public string Description { get; set; }
    public DateTime Deadline { get; set; }
    public IFormFile? Image { get; set; }
}