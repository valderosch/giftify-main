using AutoMapper;
using content_service.Dtos.goal;
using content_service.Models;
using content_service.Repositories;

namespace content_service.Services;

public class GoalService
{
    private readonly GoalRepository _goalRepository;
    private readonly IMapper _mapper;

    public GoalService(GoalRepository goalRepository, IMapper mapper)
    {
        _goalRepository = goalRepository;
        _mapper = mapper;
    }

    
    
    public async Task<List<GoalDto>> GetAllGoalsAsync()
    {
        var goals = await _goalRepository.GetAllGoalsAsync();
        return _mapper.Map<List<GoalDto>>(goals);
    }

    public async Task<GoalDto> CreateGoalAsync(CreateGoalDto createGoalDto)
    {
        var goal = _mapper.Map<Goal>(createGoalDto);
        goal.CreatedAt = DateTime.UtcNow;
        await _goalRepository.AddGoalAsync(goal);
        return _mapper.Map<GoalDto>(goal);  
    }

    public async Task<GoalDto?> UpdateGoalAsync(int id, UpdateGoalDto updateGoalDto)
    {
        var goal = await _goalRepository.GetGoalByIdAsync(id);
        if (goal == null) return null;

        _mapper.Map(updateGoalDto, goal);
        await _goalRepository.UpdateGoalAsync(goal);
        return _mapper.Map<GoalDto>(goal); 
    }

    public async Task<bool> DeleteGoalAsync(int id)
    {
        var goal = await _goalRepository.GetGoalByIdAsync(id);
        if (goal == null) return false;

        await _goalRepository.DeleteGoalAsync(goal);
        return true;
    }
}