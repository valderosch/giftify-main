using System.Text.Json;
using creator_service.Dtos.comments;
using creator_service.Dtos.user;
using creator_service.Interfaces;
using creator_service.Models;

namespace creator_service.Services;

public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<IEnumerable<CommentDto>> GetAllCommentsAsync(int postId)
        {
            var comments = await _commentRepository.GetByPostIdAsync(postId);
            return comments.Select(c => new CommentDto
            {
                Id = c.Id,
                PostId = c.PostId,
                UserId = c.UserId,  
                Content = c.Content,
                CreatedAt = c.CreatedAt
            });
        }

        public async Task<CommentDto> GetCommentByIdAsync(int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null) return null;

            return new CommentDto
            {
                Id = comment.Id,
                PostId = comment.PostId,
                UserId = comment.UserId,
                Content = comment.Content,
                CreatedAt = comment.CreatedAt
            };
        }

        public async Task<CommentDto> CreateCommentAsync(CreateCommentDto dto)
        {
            var userId = await GetUserIdByEmailAsync(dto.Email);
            Console.WriteLine($"Before Save: UserId = {userId}, PostId = {dto.PostId}, Content = {dto.Content}");
            var comment = new Comment
            {
                PostId = dto.PostId,
                UserId = userId,
                Content = dto.Content,
                CreatedAt = DateTime.UtcNow
            };

            await _commentRepository.AddAsync(comment);
            return new CommentDto
            {
                Id = comment.Id,
                PostId = comment.PostId,
                UserId = comment.UserId,
                Content = comment.Content,
                CreatedAt = comment.CreatedAt
            };
        }

        public async Task<bool> UpdateCommentAsync(int id, UpdateCommentDto dto)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null) return false;

            comment.Content = dto.Content;
            await _commentRepository.UpdateAsync(comment);
            return true;
        }

        public async Task<bool> DeleteCommentAsync(int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null) return false;

            await _commentRepository.DeleteAsync(comment);
            return true;
        }
        
        private async Task<Guid> GetUserIdByEmailAsync(string email)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"http://localhost:5000/identity/user/GetUserProfile/profile?email={email}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var user = JsonSerializer.Deserialize<UserProfileDto>(content, options);
            Console.WriteLine(user);
            return user.Id;
        }
    }