namespace content_service.Dtos.recomendation;

public class RecommendationDto
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public DateTime CreatedAt { get; set; }
    public string AuthorName { get; set; }
}