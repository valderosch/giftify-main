namespace creator_service.Dtos.comments;

public class CreateCommentDto
{
    public int PostId { get; set; }
    public string Email { get; set; }
    public string Content { get; set; }
}