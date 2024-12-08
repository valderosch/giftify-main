using creator_service.Dtos.collections;

namespace creator_service.Interfaces;

public interface ICollectionService
{
    Task<IEnumerable<CollectionDto>> GetAllCollectionsAsync(Guid  userId);
    Task<CollectionDto> GetCollectionByIdAsync(int id);
    Task<CollectionDto> CreateCollectionAsync(CreateCollectionDto dto);
    Task<bool> UpdateCollectionAsync(int id, UpdateCollectionDto dto);
    Task<bool> DeleteCollectionAsync(int id);
}