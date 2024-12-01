namespace mail_service.Models;

public class MailQueue
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsSent { get; set; }
}
