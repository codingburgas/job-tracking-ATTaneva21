using JobTracking.Application.Contracts.Base;
using JobTracking.Domain.DTOs;
using JobTracking.Domain.DTOs.Response;
using JobTracking.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JobTracking.Application.Implementation;

public class UserService : IUserService
{
    private readonly DependencyProvider _provider;

    public UserService(DependencyProvider provider)
    {
        _provider = provider;
    }

    public async Task<UserResponseDTO?> GetUserByIdAsync(int id)
    {
        var user = await _provider.Db.Users.FindAsync(id);
        return user == null ? null : MapToDto(user);
    }

    public async Task<UserResponseDTO> CreateUserAsync(CreateUserDTO dto)
    {
        var hasher = new PasswordHasher<User>();
        var user = new User
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            Address = dto.Address,
            City = dto.City,
            DateOfBirth = dto.DateOfBirth,
            Education = dto.Education,
            WorkExperience = dto.WorkExperience,
            Role = dto.Role,
            DateRegistered = DateTime.UtcNow
        };
        
        user.PasswordHash = hasher.HashPassword(user, dto.Password);

        _provider.Db.Users.Add(user);
        await _provider.Db.SaveChangesAsync();

        return MapToDto(user);
    }

    public async Task<UserResponseDTO?> UpdateUserAsync(int id, UpdateUserDTO dto)
    {
        var user = await _provider.Db.Users.FindAsync(id);
        if (user == null) return null;

        user.FirstName = dto.FirstName ?? user.FirstName;
        user.LastName = dto.LastName ?? user.LastName;
        user.Email = dto.Email ?? user.Email;
        user.PhoneNumber = dto.PhoneNumber ?? user.PhoneNumber;
        user.Address = dto.Address ?? user.Address;
        user.City = dto.City ?? user.City;
        user.DateOfBirth = dto.DateOfBirth ?? user.DateOfBirth;
        user.Education = dto.Education ?? user.Education;
        user.WorkExperience = dto.WorkExperience ?? user.WorkExperience;
        user.Role = dto.Role.HasValue ? dto.Role.Value : user.Role;  

        await _provider.Db.SaveChangesAsync();
        return MapToDto(user);
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var user = await _provider.Db.Users.FindAsync(id);
        if (user == null) return false;

        _provider.Db.Users.Remove(user);
        await _provider.Db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UserExistsAsync(int id)
    {
        return await _provider.Db.Users.AnyAsync(u => u.Id == id);
    }

    private static UserResponseDTO MapToDto(User user)
    {
        return new UserResponseDTO
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Address = user.Address,
            City = user.City,
            DateOfBirth = user.DateOfBirth,
            Education = user.Education,
            WorkExperience = user.WorkExperience,
            Role = user.Role.ToString(),
            DateRegistered = user.DateRegistered
        };
    }
}
