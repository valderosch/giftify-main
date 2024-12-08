using creator_service.Dtos.collections;
using creator_service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace creator_service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CollectionsController : ControllerBase
{
    private readonly ICollectionService _collectionService;

    public CollectionsController(ICollectionService collectionService)
    {
        _collectionService = collectionService;
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<IEnumerable<CollectionDto>>> GetCollectionsByUserId(Guid  userId)
    {
        var collections = await _collectionService.GetAllCollectionsAsync(userId);
        return Ok(collections);
    }

    [HttpPost]
    public async Task<ActionResult<CollectionDto>> CreateCollection([FromBody] CreateCollectionDto dto)
    {
        var collection = await _collectionService.CreateCollectionAsync(dto);
        return CreatedAtAction(nameof(GetCollectionsByUserId), new { userId = collection.UserId }, collection);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCollection(int id, [FromBody] UpdateCollectionDto dto)
    {
        var result = await _collectionService.UpdateCollectionAsync(id, dto);
        if (!result) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCollection(int id)
    {
        var result = await _collectionService.DeleteCollectionAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }
}