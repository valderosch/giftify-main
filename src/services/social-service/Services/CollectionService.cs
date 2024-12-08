using creator_service.Dtos.collections;
using creator_service.Interfaces;
using creator_service.Models;

namespace creator_service.Services;

public class CollectionService : ICollectionService
{
    private readonly ICollectionRepository _collectionRepository;

    public CollectionService(ICollectionRepository collectionRepository)
    {
        _collectionRepository = collectionRepository;
    }

    public async Task<IEnumerable<CollectionDto>> GetAllCollectionsAsync(Guid  userId)
    {
        var collections = await _collectionRepository.GetByUserIdAsync(userId); 
        return collections.Select(c => new CollectionDto
        {
            Id = c.Id,
            UserId = c.UserId, 
            Name = c.Name,
            Description = c.Description
        });
    }

    public async Task<CollectionDto> GetCollectionByIdAsync(int id)
    {
        var collection = await _collectionRepository.GetByIdAsync(id);
        if (collection == null) return null;

        return new CollectionDto
        {
            Id = collection.Id,
            UserId = collection.UserId,
            Name = collection.Name,
            Description = collection.Description
        };
    }

    public async Task<CollectionDto> CreateCollectionAsync(CreateCollectionDto dto)
    {
        var collection = new Collection
        {
            UserId = dto.UserId,
            Name = dto.Name,
            Description = dto.Description
        };

        await _collectionRepository.AddAsync(collection);
        return new CollectionDto
        {
            Id = collection.Id,
            UserId = collection.UserId,
            Name = collection.Name,
            Description = collection.Description
        };
    }

    public async Task<bool> UpdateCollectionAsync(int id, UpdateCollectionDto dto)
    {
        var collection = await _collectionRepository.GetByIdAsync(id);
        if (collection == null) return false;

        collection.Name = dto.Name;
        collection.Description = dto.Description;
        await _collectionRepository.UpdateAsync(collection);
        return true;
    }

    public async Task<bool> DeleteCollectionAsync(int id)
    {
        var collection = await _collectionRepository.GetByIdAsync(id);
        if (collection == null) return false;

        await _collectionRepository.DeleteAsync(collection);
        return true;
    }
}