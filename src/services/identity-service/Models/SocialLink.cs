namespace identity_service.Models;

public class SocialLink
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Platform { get; set; } = null!;
    public string Url { get; set; } = null!;
}
