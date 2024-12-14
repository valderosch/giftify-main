namespace content_service.Dtos.post;

public class PostDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsPublic { get; set; }
    public int Likes { get; set; }
    public int PlanId { get; set; } 
    public decimal Price { get; set; } 
    public List<int>? FileIds { get; set; }
}

public class FileAttachmentDto
{
    public string FilePath { get; set; }
    public string FileType { get; set; }
}