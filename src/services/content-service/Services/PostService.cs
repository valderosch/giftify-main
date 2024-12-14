using AutoMapper;
using content_service.Dtos.post;
using content_service.Models;
using content_service.Repositories;

namespace content_service.Services;

public class PostService
{
    private readonly PostRepository _postRepository;
    private readonly UserSubscriptionRepository _subscriptionRepository;
    private readonly IMapper _mapper;

    public PostService(PostRepository postRepository, UserSubscriptionRepository subscriptionRepository, IMapper mapper)
    {
        _postRepository = postRepository;
        _subscriptionRepository = subscriptionRepository;
        _mapper = mapper;
    }

    public async Task<PostDto?> GetPostByIdAsync(int id, Guid userId)
    {
        var post = await _postRepository.GetPostByIdAsync(id);
        if (post == null) return null;

        bool hasAccess = await _subscriptionRepository.IsSubscribedAsync(userId, post.AuthorId);
        if (!post.IsPublic && !hasAccess) throw new UnauthorizedAccessException("Access denied.");

        return _mapper.Map<PostDto>(post);
    }

    public async Task<List<PostDto>> GetAllPostsAsync(Guid userId)
    {
        var posts = await _postRepository.GetAllPostsAsync(userId);
        return _mapper.Map<List<PostDto>>(posts);
    }

    public async Task<PostDto> CreatePostAsync(CreatePostDto createPostDto, Guid userId)
    {
        var post = _mapper.Map<Post>(createPostDto);
        post.AuthorId = userId;
        post.CreatedAt = DateTime.UtcNow;

        await _postRepository.AddPostAsync(post);
        return _mapper.Map<PostDto>(post);
    }

    public async Task<PostDto?> UpdatePostAsync(int id, UpdatePostDto updatePostDto)
    {
        var post = await _postRepository.GetPostByIdAsync(id);
        if (post == null) return null;

        _mapper.Map(updatePostDto, post);
        await _postRepository.UpdatePostAsync(post);
        return _mapper.Map<PostDto>(post);
    }

    public async Task<bool> DeletePostAsync(int id)
    {
        var post = await _postRepository.GetPostByIdAsync(id);
        if (post == null) return false;

        await _postRepository.DeletePostAsync(post);
        return true;
    }

    public async Task<bool> LikePostAsync(int id)
    {
        var post = await _postRepository.GetPostByIdAsync(id);
        if (post == null) return false;

        post.Likes += 1;
        await _postRepository.UpdatePostAsync(post);
        return true;
    }
}