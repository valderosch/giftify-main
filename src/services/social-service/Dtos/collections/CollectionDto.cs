namespace creator_service.Dtos.collections;

public class CollectionDto
{
    public int Id { get; set; }
    public Guid  UserId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<int> PostIds { get; set; }
}