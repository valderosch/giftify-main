using AutoMapper;
using content_service.Dtos.post;
using content_service.Repositories;

namespace content_service.Services;

public class RecommendationService
{
    private readonly PostRepository _postRepository;
    private readonly UserSubscriptionRepository _subscriptionRepository;
    private readonly IMapper _mapper;

    public RecommendationService(PostRepository postRepository, UserSubscriptionRepository subscriptionRepository, IMapper mapper)
    {
        _postRepository = postRepository;
        _subscriptionRepository = subscriptionRepository;
        _mapper = mapper;
    }

    public async Task<List<PostDto>> GetRecommendationsAsync(Guid userId)
    {
        var subscribedAuthors = await _subscriptionRepository.GetSubscribedAuthorIdsAsync(userId); 
        var posts = await _postRepository.GetAllPostsAsync(userId);
        return posts
            .Where(p => subscribedAuthors.Contains(p.AuthorId)) 
            .Select(p => _mapper.Map<PostDto>(p))
            .ToList();
    }
}