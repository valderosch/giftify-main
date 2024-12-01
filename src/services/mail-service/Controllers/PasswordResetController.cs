using mail_service.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace mail_service.Controllers;

[ApiController]
[Route("mailing/[controller]")]
public class PasswordResetController : ControllerBase
{
    private readonly EmailService _emailService;

    public PasswordResetController(EmailService emailService)
    {
        _emailService = emailService;
    }
    
    [HttpPost("send-password-reset")]
    public async Task<IActionResult> SendPasswordResetEmail([FromBody] PasswordResetRequestDto request)
    {
        if (string.IsNullOrEmpty(request.Email))
            return BadRequest(new { Message = "Email is required." });

        var resetToken = Guid.NewGuid().ToString();
        var resetLink = $"https://example.com/reset-password?token={resetToken}";

        var subject = "Password Reset Request";
        var body = $"Click <a href='{resetLink}'>here</a> to reset your password. New link";

        await _emailService.SendEmailAsync(request.Email, subject, body);

        return Ok(new { Message = "Password reset email sent! horay!" });
    }

    [HttpPost]
    public async Task<IActionResult> ResetPassword([FromBody] PasswordResetRequestDto request)
    {
        var resetLink = $"https://example.com/reset-password?token={Guid.NewGuid()}";

        var subject = "Password Reset Request";
        var body = $"Click <a href='{resetLink}'>here</a> to reset your password.";

        await _emailService.SendEmailAsync(request.Email, subject, body);

        return Ok(new { Message = "Password reset email sent!" });
    }
    
    [HttpPost("send")]
    public async Task<IActionResult> SendMail([FromBody] MailMessageDto request)
    {
        if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Subject) || string.IsNullOrEmpty(request.Body))
            return BadRequest(new { Message = "Invalid email request data." });
        
        await _emailService.SendEmailAsync(request.Email, request.Subject, request.Body);

        return Ok(new { Message = "Email sent successfully!" });
    }
}