using content_service.Dtos;
using content_service.Dtos.goal;
using content_service.Services;
using Microsoft.AspNetCore.Mvc;

namespace content_service.Controllers;

[ApiController]
[Route("content/[action]")]
public class GoalController : ControllerBase
{
    private readonly GoalService _goalService;
    private readonly IHttpClientFactory _httpClientFactory;

    public GoalController(GoalService goalService, IHttpClientFactory httpClientFactory)
    {
        _goalService = goalService;
        _httpClientFactory = httpClientFactory;
    }

    private async Task<Guid?> GetUserIdByEmailAsync(string email)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var response = await httpClient.GetAsync($"http://localhost:5000/identity/user/GetUserProfile/profile?email={email}");
        if (!response.IsSuccessStatusCode) return null;

        var userProfile = await response.Content.ReadFromJsonAsync<UserProfileDto>();
        return userProfile?.Id;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllGoals()
    {
        var goals = await _goalService.GetAllGoalsAsync();
        return Ok(goals);
    }

    [HttpGet("author/{authorId}")]
    public async Task<IActionResult> GetGoalsByAuthor(Guid authorId)
    {
        var goals = await _goalService.GetGoalsByAuthorAsync(authorId);
        return Ok(goals);
    }

    [HttpPost]
    public async Task<IActionResult> CreateGoal([FromForm] CreateGoalDto dto, [FromQuery] string email)
    {
        var userId = await GetUserIdByEmailAsync(email);
        if (userId == null) return Unauthorized("User not found.");

        var createdGoal = await _goalService.CreateGoalAsync(dto, userId.Value);
        return CreatedAtAction(nameof(GetAllGoals), new { id = createdGoal.Id }, createdGoal);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGoal(int id, [FromForm] UpdateGoalDto dto)
    {
        var updatedGoal = await _goalService.UpdateGoalAsync(id, dto);
        if (updatedGoal == null)
            return NotFound("Goal not found.");
        return Ok(updatedGoal);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGoal(int id)
    {
        var result = await _goalService.DeleteGoalAsync(id);
        if (!result)
            return NotFound("Goal not found.");
        return NoContent();
    }
}