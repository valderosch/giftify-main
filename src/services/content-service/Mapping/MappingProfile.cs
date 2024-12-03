using AutoMapper;
using content_service.Dtos.goal;
using content_service.Dtos.post;
using content_service.Models;

namespace content_service.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Post, PostDto>().ReverseMap();
        CreateMap<CreatePostDto, Post>();
        CreateMap<UpdatePostDto, Post>();

        CreateMap<Goal, GoalDto>().ReverseMap();
        CreateMap<CreateGoalDto, Goal>();
        CreateMap<UpdateGoalDto, Goal>();
    }
}