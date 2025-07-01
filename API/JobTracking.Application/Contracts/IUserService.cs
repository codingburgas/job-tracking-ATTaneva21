using JobTracking.Domain.DTOs;
using JobTracking.Domain.DTOs.Response;

public interface IUserService
{
    Task<UserResponseDTO?> GetUserByIdAsync(int id);
    Task<UserResponseDTO> CreateUserAsync(CreateUserDTO dto);
    Task<UserResponseDTO?> UpdateUserAsync(int id, UpdateUserDTO dto);
    Task<bool> DeleteUserAsync(int id);
    Task<bool> UserExistsAsync(int id);
}