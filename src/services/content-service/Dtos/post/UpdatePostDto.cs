namespace content_service.Dtos.post;

public class UpdatePostDto
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    public bool? IsPublic { get; set; }
}