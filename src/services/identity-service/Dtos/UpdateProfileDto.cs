namespace identity_service.Dtos;

public class UpdateProfileDto
{
    public string Username { get; set; } = null!;
    public string ShortDescription { get; set; } = "";
    public string LongDescription { get; set; } = "";
}