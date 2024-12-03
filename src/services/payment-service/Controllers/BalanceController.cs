using Microsoft.AspNetCore.Mvc;
using payment_service.Dtos;
using payment_service.Interfaces;

namespace payment_service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BalanceController : ControllerBase
{
    private readonly IBalanceService _balanceService;

    public BalanceController(IBalanceService balanceService)
    {
        _balanceService = balanceService;
    }
    
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetBalance(Guid userId)
    {
        var result = await _balanceService.GetBalanceAsync(userId);
        if (result.IsSuccess)
            return Ok(result.Balance);

        return NotFound(result.ErrorMessage);
    }
    
    [HttpPost("top-up")]
    public async Task<IActionResult> TopUpBalance([FromBody] TopUpBalanceDto dto)
    {
        var result = await _balanceService.TopUpBalanceAsync(dto);
        if (result.IsSuccess)
            return Ok(result.Message);

        return BadRequest(result.ErrorMessage);
    }
    
    [HttpPost("withdraw")]
    public async Task<IActionResult> WithdrawBalance([FromBody] WithdrawBalanceDto dto)
    {
        var result = await _balanceService.WithdrawBalanceAsync(dto);
        if (result.IsSuccess)
            return Ok(result.Message);

        return BadRequest(result.ErrorMessage);
    }
}