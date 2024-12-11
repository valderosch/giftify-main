using identity_service.Dtos;
using IdentityService.Repositories;

public class UserService
{
    private readonly UserRepository _userRepository;

    public UserService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserProfileDto?> GetUserProfileAsync(string email)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);
        if (user == null) return null;

        return new UserProfileDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            ShortDescription = user.ShortDescription,
            LongDescription = user.LongDescription
        };
    }
    
    public async Task<UserProfileDto?> GetUserProfileByIdAsync(Guid id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        if (user == null) return null;

        return new UserProfileDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            ShortDescription = user.ShortDescription,
            LongDescription = user.LongDescription
        };
    }

    public async Task<string> UpdateUserProfileAsync(string email, UpdateProfileDto updateDto)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);
        if (user == null) return "User not found in system.";

        user.Username = updateDto.Username;
        user.ShortDescription = updateDto.ShortDescription;
        user.LongDescription = updateDto.LongDescription;

        await _userRepository.UpdateUserAsync(user);
        return "Profile updated successfully. Changes applied.";
    }
}