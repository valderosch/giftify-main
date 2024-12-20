using System.Security.Claims;
using identity_service.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("user/[action]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet("profile")]
    public async Task<IActionResult> GetUserProfile(string email)
    {
        var userProfile = await _userService.GetUserProfileAsync(email);
        return userProfile != null ? Ok(userProfile) : NotFound("User not found.");
    }
    
    [HttpGet("profile/{id:guid}")]
    public async Task<IActionResult> GetUserProfileById(Guid id)
    {
        var userProfile = await _userService.GetUserProfileByIdAsync(id);
        return userProfile != null ? Ok(userProfile) : NotFound("User not found.");
    }

    [HttpPut("profile")]
    public async Task<IActionResult> UpdateUserProfile(string email, UpdateProfileDto updateDto)
    {
        var result = await _userService.UpdateUserProfileAsync(email, updateDto);
        return Ok(result);
    }
}