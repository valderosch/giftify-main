using content_service.Dtos;
using content_service.Dtos.post;
using content_service.Services;
using Microsoft.AspNetCore.Mvc;

namespace content_service.Controllers;

[ApiController]
[Route("api/posts")]
public class PostController : ControllerBase
{
    private readonly PostService _postService;
    private readonly HttpClient _httpClient;

    public PostController(PostService postService, IHttpClientFactory httpClientFactory)
    {
        _postService = postService;
        _httpClient = httpClientFactory.CreateClient();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPostById(int id, [FromQuery] Guid userId)
    {
        var post = await _postService.GetPostByIdAsync(id, userId);
        if (post == null)
            return NotFound("Post not found.");
        return Ok(post);
    }

    private async Task<Guid?> GetUserIdByEmailAsync(string email)
    {
        var response = await _httpClient.GetAsync($"http://localhost:5000/identity/user/GetUserProfile/profile?email={email}");
        if (!response.IsSuccessStatusCode) return null;

        var userProfile = await response.Content.ReadFromJsonAsync<UserProfileDto>();
        return userProfile?.Id;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPosts([FromQuery] string email)
    {
        var userId = await GetUserIdByEmailAsync(email);
        if (userId == null) return Unauthorized("User not found.");

        var posts = await _postService.GetAllPostsAsync(userId.Value);
        return Ok(posts);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] CreatePostDto dto, [FromQuery] string email)
    {
        var userId = await GetUserIdByEmailAsync(email);
        if (userId == null) return Unauthorized("User not found.");

        var post = await _postService.CreatePostAsync(dto, userId.Value);
        return CreatedAtAction(nameof(GetPostById), new { id = post.Id.ToString() }, post);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePost(int id, [FromBody] UpdatePostDto dto)
    {
        var updatedPost = await _postService.UpdatePostAsync(id, dto);
        if (updatedPost == null)
            return NotFound("Post not found.");
        return Ok(updatedPost);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost(int id)
    {
        var result = await _postService.DeletePostAsync(id);
        if (!result)
            return NotFound("Post not found.");
        return NoContent();
    }

    [HttpPost("{id}/like")]
    public async Task<IActionResult> LikePost(int id)
    {
        var result = await _postService.LikePostAsync(id);
        return result ? Ok("Post liked.") : NotFound("Post not found.");
    }
}