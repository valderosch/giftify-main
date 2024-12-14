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

    public async Task<List<GoalDto>> GetGoalsByAuthorAsync(Guid authorId)
    {
        var goals = await _goalRepository.GetGoalsByAuthorAsync(authorId);
        return _mapper.Map<List<GoalDto>>(goals);
    }

    public async Task<GoalDto> CreateGoalAsync(CreateGoalDto dto, Guid authorId)
    {
        var goal = _mapper.Map<Goal>(dto);
        goal.AuthorId = authorId;

        if (dto.Image != null)
        {
            var imagePath = await SaveImageAsync(dto.Image);
            goal.ImagePath = imagePath;
        }

        await _goalRepository.AddGoalAsync(goal);
        return _mapper.Map<GoalDto>(goal);
    }

    public async Task<GoalDto?> UpdateGoalAsync(int id, UpdateGoalDto dto)
    {
        var goal = await _goalRepository.GetGoalByIdAsync(id);
        if (goal == null) return null;

        _mapper.Map(dto, goal);

        if (dto.Image != null)
        {
            var imagePath = await SaveImageAsync(dto.Image);
            goal.ImagePath = imagePath;
        }

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

    public async Task<string> SaveImageAsync(IFormFile image)
    {
        string imagesFolder = Path.Combine(Directory.GetCurrentDirectory(), "file-storage");
        if (!Directory.Exists(imagesFolder))
        {
            Directory.CreateDirectory(imagesFolder);
        }
        string uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
        string filePath = Path.Combine(imagesFolder, uniqueFileName);
        
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await image.CopyToAsync(fileStream);
        }

        return uniqueFileName;
    }
}