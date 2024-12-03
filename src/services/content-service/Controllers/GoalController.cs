using content_service.Dtos.goal;
using content_service.Services;
using Microsoft.AspNetCore.Mvc;

namespace content_service.Controllers;

[ApiController]
[Route("api/goals")]
public class GoalController : ControllerBase
{
    private readonly GoalService _goalService;

    public GoalController(GoalService goalService)
    {
        _goalService = goalService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllGoals()
    {
        var goals = await _goalService.GetAllGoalsAsync();
        return Ok(goals);
    }

    [HttpPost]
    public async Task<IActionResult> CreateGoal([FromBody] CreateGoalDto dto)
    {
        var createdGoal = await _goalService.CreateGoalAsync(dto);
        return CreatedAtAction(nameof(GetAllGoals), new { id = createdGoal.Id }, createdGoal);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGoal(int id, [FromBody] UpdateGoalDto dto)
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
