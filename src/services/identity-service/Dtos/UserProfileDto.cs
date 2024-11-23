namespace identity_service.Dtos;

public class UserProfileDto
{
    public Guid Id { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string ShortDescription { get; set; } = "";
    public string LongDescription { get; set; } = "";
}