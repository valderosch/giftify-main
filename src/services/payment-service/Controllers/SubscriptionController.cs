using Microsoft.AspNetCore.Mvc;
using payment_service.Dtos;
using payment_service.Interfaces;
using payment_service.Services;

namespace payment_service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubscriptionController : ControllerBase
{
    private readonly ISubscriptionService _subscriptionService;

    public SubscriptionController(ISubscriptionService subscriptionService)
    {
        _subscriptionService = subscriptionService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSubscription([FromBody] CreateSubscriptionDto dto)
    {
        var result = await _subscriptionService.CreateSubscriptionAsync(dto);
        if (result != null)
            return CreatedAtAction(nameof(GetSubscriptionById), new { id = result.Id }, result);
        return BadRequest("Unable to create subscription.");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSubscriptionById(Guid id)
    {
        var subscription = await _subscriptionService.GetSubscriptionByIdAsync(id);
        if (subscription == null)
            return NotFound();
        return Ok(subscription);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSubscription(Guid id, [FromBody] UpdateSubscriptionDto dto)
    {
        var updated = await _subscriptionService.UpdateSubscriptionAsync(id, dto);
        if (updated)
            return NoContent();
        return NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSubscription(Guid id)
    {
        var deleted = await _subscriptionService.DeleteSubscriptionAsync(id);
        if (deleted)
            return NoContent();
        return NotFound();
    }
}