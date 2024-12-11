using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using identity_service.Dtos;
using identity_service.Dtos.author;
using identity_service.Models;
using IdentityService.DTOs;
using IdentityService.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace identity_service.Services;
public class AuthService
{
    private readonly UserRepository _userRepository;
    private readonly IConfiguration _configuration;
    private readonly MailServiceClient _mailServiceClient;

    public AuthService(UserRepository userRepository, IConfiguration configuration, MailServiceClient mailServiceClient)
    {
        _userRepository = userRepository;
        _configuration = configuration;
        _mailServiceClient = mailServiceClient;
    }

    public async Task<string> RegisterUserAsync(RegisterDto registerDto)
    {
        var existingUser = await _userRepository.GetUserByEmailAsync(registerDto.Email);
        if (existingUser != null) return "User with this email already exists in system.";

        var user = new User
        {
            Username = registerDto.Username,
            Email = registerDto.Email,
            PasswordHash = HashPassword(registerDto.Password),
            Roles = new List<Role> { new Role { Name = "User" } },
            SocialLinks = new List<SocialLink>
            {
                new SocialLink
                {
                    Platform = "GitHub",
                    Url = "https://github.com/valderosch"
                }
            },
            ShortDescription = "I am a newbie"
        };

        await _userRepository.AddUserAsync(user);
        return "Registration successful. New user was created.";
    }
    

    public async Task<LoginResponseDto?> LoginAsync(LoginDto loginDto)
    {
        var user = await _userRepository.GetUserByEmailAsync(loginDto.Email);
        if (user == null || !VerifyPassword(loginDto.Password, user.PasswordHash))
            return null;

        var token = GenerateJwtToken(user);
        return new LoginResponseDto
        {
            Token = token,
            User = new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                ShortDescription = user.ShortDescription,
                SocialLinks = user.SocialLinks
            }
        };
    }

    private string GenerateJwtToken(User user)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, "User")
        };

        var tokenOptions = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(60),
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }
    
    public async Task<string> RequestPasswordResetAsync(PasswordResetRequestDto requestDto)
    {
        var user = await _userRepository.GetUserByEmailAsync(requestDto.Email);
        if (user == null) return "User not found.";
        
        var emailSent = await _mailServiceClient.SendPasswordResetEmailAsync(requestDto);

        return emailSent ? "Password reset link sent to your email." : "Failed to send email.";
    }

    public async Task<string> ResetPasswordAsync(ResetPasswordDto resetDto)
    {
        var user = await _userRepository.GetUserByEmailAsync(resetDto.Email);
        if (user == null) return "User not found.";

        user.PasswordHash = HashPassword(resetDto.NewPassword);
        await _userRepository.UpdateUserAsync(user);
        return "Password reset successful.";
    }

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        return Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(password)));
    }

    private bool VerifyPassword(string password, string hashedPassword)
    {
        return HashPassword(password) == hashedPassword;
    }
}