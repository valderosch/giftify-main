namespace creator_service.Dtos.user;

public class UserProfileDto
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string ShortDescription { get; set; }
    public string LongDescription { get; set; }
}