using creator_service.Dtos.comments;
using creator_service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace creator_service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentsController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpGet("{postId}")]
    public async Task<ActionResult<IEnumerable<CommentDto>>> GetCommentsByPostId(int postId)
    {
        var comments = await _commentService.GetAllCommentsAsync(postId);
        return Ok(comments);
    }

    [HttpPost]
    public async Task<ActionResult<CommentDto>> CreateComment([FromBody] CreateCommentDto dto)
    {
        var comment = await _commentService.CreateCommentAsync(dto);
        return CreatedAtAction(nameof(GetCommentsByPostId), new { postId = comment.PostId }, comment);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateComment(int id, [FromBody] UpdateCommentDto dto)
    {
        var result = await _commentService.UpdateCommentAsync(id, dto);
        if (!result) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComment(int id)
    {
        var result = await _commentService.DeleteCommentAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }
}