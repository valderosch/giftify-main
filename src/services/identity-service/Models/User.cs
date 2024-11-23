namespace identity_service.Models;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string ShortDescription { get; set; } = "";
    public string LongDescription { get; set; } = "";
    public int PostCount { get; set; } = 0;
    public int FollowerCount { get; set; } = 0;
    public List<SocialLink> SocialLinks { get; set; } = new();
    public List<Role> Roles { get; set; } = new();
}
