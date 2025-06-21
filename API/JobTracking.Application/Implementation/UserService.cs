using JobTracking.Application.Contracts;
using JobTracking.Application.Contracts.Base;
using JobTracking.Domain.DTOs;
using JobTracking.Domain.Enums;
using JobTracking.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CandidateFilter = JobTracking.Domain.Filters.CandidateFilter;

namespace JobTracking.Application.Implementation;

public class UserService : IUserService
{
    private readonly DependencyProvider _provider;

    public UserService(DependencyProvider provider)
    {
        _provider = provider;
    }

    public async Task<UserResponseDTO> GetUserAsync(int id)
    {
        var user = await _provider.Db.Users.FindAsync(id);
        
        if (user == null)
            throw new Exception("User not found");

        return MapToResponseDto(user);
    }

    public async Task<List<UserResponseDTO>> GetAllUsersAsync()
    {
        var users = await _provider.Db.Users.ToListAsync();
        return users.Select(MapToResponseDto).ToList();
    }

    public async Task<List<UserResponseDTO>> GetFilteredUsersAsync(BaseFilter<CandidateFilter> filter)
    {
        var query = _provider.Db.Users.AsQueryable();

        if (filter.Filter != null)  
        {
            if (!string.IsNullOrEmpty(filter.Filter.FirstName))
                query = query.Where(u => u.FirstName.Contains(filter.Filter.FirstName));
                
            if (!string.IsNullOrEmpty(filter.Filter.LastName))
                query = query.Where(u => u.LastName.Contains(filter.Filter.LastName));
                
            if (!string.IsNullOrEmpty(filter.Filter.Email))
                query = query.Where(u => u.Email.Contains(filter.Filter.Email));
        }
        
        if (!string.IsNullOrEmpty(filter.SortBy))
        {
            switch (filter.SortBy.ToLower())
            {
                case "firstname":
                    query = filter.SortDirection == SortOrderEnum.DESC 
                        ? query.OrderByDescending(u => u.FirstName)
                        : query.OrderBy(u => u.FirstName);
                    break;
                case "lastname":
                    query = filter.SortDirection == SortOrderEnum.DESC 
                        ? query.OrderByDescending(u => u.LastName)
                        : query.OrderBy(u => u.LastName);
                    break;
                case "email":
                    query = filter.SortDirection == SortOrderEnum.DESC 
                        ? query.OrderByDescending(u => u.Email)
                        : query.OrderBy(u => u.Email);
                    break;
                default:
                    query = query.OrderBy(u => u.Id);
                    break;
            }
        }
        query = query.Skip((filter.Page - 1) * filter.PageSize).Take(filter.PageSize);

        var users = await query.ToListAsync();
        return users.Select(MapToResponseDto).ToList();
    }

    public async Task<UserResponseDTO> CreateUserAsync(CreateUserDto dto)
    {
        var user = new User
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PasswordHash = dto.PasswordHash,
            PhoneNumber = dto.PhoneNumber,
            Address = dto.Address,
            City = dto.City,
            ZipCode = dto.ZipCode,
            Country = dto.Country,
            DateOfBirth = dto.DateOfBirth,
            Education = dto.Education,
            WorkExperience = dto.WorkExperience,
            Role = dto.Role,
            DateRegistered = DateTime.UtcNow
        };

        _provider.Db.Users.Add(user);
        await _provider.Db.SaveChangesAsync();

        return MapToResponseDto(user);
    }

    public async Task<UserResponseDTO> UpdateUserAsync(int id, UpdateUserDto dto)
    {
        var user = await _provider.Db.Users.FindAsync(id);
        
        if (user == null)
            throw new Exception("User not found");

        user.FirstName = dto.FirstName;
        user.LastName = dto.LastName;
        user.Email = dto.Email;
        user.PhoneNumber = dto.PhoneNumber;
        user.Address = dto.Address;
        user.City = dto.City;
        user.ZipCode = dto.ZipCode;
        user.Country = dto.Country;
        user.DateOfBirth = dto.DateOfBirth;
        user.Education = dto.Education;
        user.WorkExperience = dto.WorkExperience;
        user.Role = dto.Role;

        await _provider.Db.SaveChangesAsync();

        return MapToResponseDto(user);
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var user = await _provider.Db.Users.FindAsync(id);
        
        if (user == null)
            return false;

        _provider.Db.Users.Remove(user);
        await _provider.Db.SaveChangesAsync();
        
        return true;
    }

    private UserResponseDTO MapToResponseDto(User user)
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
            ZipCode = user.ZipCode,
            Country = user.Country,
            DateOfBirth = user.DateOfBirth,
            DateRegistered = user.DateRegistered,
            Education = user.Education,
            WorkExperience = user.WorkExperience,
            Role = user.Role.ToString()
        };
    }
}