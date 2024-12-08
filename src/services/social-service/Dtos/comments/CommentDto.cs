namespace creator_service.Dtos.comments;

public class CommentDto
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public Guid  UserId { get; set; } 
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
}