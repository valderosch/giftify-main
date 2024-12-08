namespace creator_service.Dtos.collections;

public class CreateCollectionDto
{
    public Guid  UserId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}