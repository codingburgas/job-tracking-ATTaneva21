using JobTracking.Domain.DTOs;

namespace JobTracking.Application.Contracts;

public interface IApplicationService
{
    Task<ApplicationResponseDTO?> GetByIdAsync(int id);
    Task<ApplicationResponseDTO> CreateAsync(CreateApplicationDTO dto);
    Task<ApplicationResponseDTO?> UpdateAsync(int id, UpdateApplicationDTO dto);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
