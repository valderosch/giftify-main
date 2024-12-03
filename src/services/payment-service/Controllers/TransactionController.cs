using Microsoft.AspNetCore.Mvc;
using payment_service.Dtos;
using payment_service.Interfaces;
using payment_service.Services;

namespace payment_service.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionDto dto)
    {
        var result = await _transactionService.CreateTransactionAsync(dto);
        if (!result.IsSuccess)
            return BadRequest(result.ErrorMessage);

        return Ok(result.Transaction);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTransaction(Guid id)
    {
        var transaction = await _transactionService.GetTransactionByIdAsync(id);
        if (transaction == null)
            return NotFound();

        return Ok(transaction);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUserTransactions(Guid userId)
    {
        var transactions = await _transactionService.GetTransactionsByUserIdAsync(userId);
        return Ok(transactions);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTransaction(Guid id)
    {
        var success = await _transactionService.DeleteTransactionAsync(id);
        if (!success)
            return NotFound();

        return NoContent();
    }
}