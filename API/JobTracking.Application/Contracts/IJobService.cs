using JobTracking.Domain.DTOs;
using JobTracking.Domain.Filters;
using JobTracking.Domain.Enums;

namespace JobTracking.Application.Contracts
{
    public interface IJobService
    {
        Task<IEnumerable<JobResponseDTO>> GetAllJobsAsync();
        Task<IEnumerable<JobResponseDTO>> GetFilteredJobsAsync(JobFilter filter);
        Task<JobResponseDTO?> GetJobByIdAsync(int id);
        Task<JobResponseDTO> CreateJobAsync(CreateJobDto createJobDto);
        Task<JobResponseDTO?> UpdateJobAsync(int id, UpdateJobDto updateJobDto);
        Task<bool> DeleteJobAsync(int id);
        Task<bool> JobExistsAsync(int id);
        Task<IEnumerable<JobResponseDTO>> GetJobsByHiringManagerAsync(int hiringManagerId);
        Task<IEnumerable<JobResponseDTO>> GetActiveJobsAsync();
        Task<bool> UpdateJobStatusAsync(int id, JobStatus status);
    }
}