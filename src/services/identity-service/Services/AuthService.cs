using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using identity_service.Dtos;
using identity_service.Models;
using IdentityService.DTOs;
using IdentityService.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace identity_service.Services;
public class AuthService
{
    private readonly UserRepository _userRepository;

    public AuthService(UserRepository userRepository)
    {
        _userRepository = userRepository;
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
            Roles = new List<Role> { new Role { Name = "User" } }
        };

        await _userRepository.AddUserAsync(user);
        return "Registration successful. New user was created.";
    }

    public async Task<string?> LoginAsync(LoginDto loginDto)
    {
        var user = await _userRepository.GetUserByEmailAsync(loginDto.Email);
        if (user == null || !VerifyPassword(loginDto.Password, user.PasswordHash))
            return null;

        return "Login successful. Data is correct.";
    }

    public async Task<string> RequestPasswordResetAsync(PasswordResetRequestDto requestDto)
    {
        var user = await _userRepository.GetUserByEmailAsync(requestDto.Email);
        if (user == null) return "User not found.";
        return "Password reset link sent to your email.";
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