using creator_service.Dtos.comments;

namespace creator_service.Interfaces;

public interface ICommentService
{
    Task<IEnumerable<CommentDto>> GetAllCommentsAsync(int postId);
    Task<CommentDto> GetCommentByIdAsync(int id);
    Task<CommentDto> CreateCommentAsync(CreateCommentDto dto);
    Task<bool> UpdateCommentAsync(int id, UpdateCommentDto dto);
    Task<bool> DeleteCommentAsync(int id);
}