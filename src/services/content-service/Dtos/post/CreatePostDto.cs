namespace content_service.Dtos.post;

public class CreatePostDto
{
    public string Title { get; set; }
    public string Content { get; set; }
    public List<int>? FileIds { get; set; } 
    public bool IsPublic { get; set; }
}