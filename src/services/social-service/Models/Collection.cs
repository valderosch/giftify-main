namespace creator_service.Models;

public class Collection
{
    public int Id { get; set; }
    public Guid  UserId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
    public ICollection<int> PostIds { get; set; } = new List<int>();
}