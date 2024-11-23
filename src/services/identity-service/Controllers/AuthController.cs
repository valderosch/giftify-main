using identity_service.Dtos;
using identity_service.Services;
using IdentityService.DTOs;
using Microsoft.AspNetCore.Mvc;



[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        var result = await _authService.RegisterUserAsync(registerDto);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var result = await _authService.LoginAsync(loginDto);
        return result != null ? Ok(result) : Unauthorized("Invalid credentials. Try another");
    }

    [HttpPost("request-reset-password")]
    public async Task<IActionResult> RequestPasswordReset(PasswordResetRequestDto requestDto)
    {
        var result = await _authService.RequestPasswordResetAsync(requestDto);
        return Ok(result);
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(ResetPasswordDto resetDto)
    {
        var result = await _authService.ResetPasswordAsync(resetDto);
        return Ok(result);
    }
}