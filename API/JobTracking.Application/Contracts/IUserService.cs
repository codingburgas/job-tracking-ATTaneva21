using JobTracking.Domain.DTOs;
using JobTracking.Domain.Filters;

namespace JobTracking.Application.Contracts
{
    public interface IUserService
    {
        Task<UserResponseDTO> GetUserAsync(int id);
        Task<List<UserResponseDTO>> GetAllUsersAsync();
        Task<List<UserResponseDTO>> GetFilteredUsersAsync(BaseFilter<CandidateFilter> filter);
        Task<UserResponseDTO> CreateUserAsync(CreateUserDto dto);
        Task<UserResponseDTO> UpdateUserAsync(int id, UpdateUserDto dto);
        Task<bool> DeleteUserAsync(int id);
    }
}