using identity_service.Models;

namespace identity_service.Dtos.author;

public class LoginResponseDto
{
    public string Token { get; set; } = null!;
    public UserDto User { get; set; } = null!;
}

public class UserDto
{
    public Guid Id { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string ShortDescription { get; set; } = null!;
    public List<SocialLink> SocialLinks { get; set; } = new();
}