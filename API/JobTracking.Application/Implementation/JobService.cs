using JobTracking.Application.Contracts;
using JobTracking.Application.Contracts.Base;
using JobTracking.Domain.DTOs;

using JobTracking.Domain.Filters;
using JobTracking.Models;
using JobStatus = JobTracking.Models.JobStatus;


namespace JobTracking.Services
{
    public class JobService : IJobService
    {
        private readonly DependencyProvider _provider;

        public UserService(DependencyProvider provider)
        {
            _provider = provider;
        }

        public async Task<IEnumerable<JobResponseDTO>> GetAllJobsAsync()
        {
            return await _provider.Jobs
                .Include(j => j.HiringManager)
                .Include(j => j.Applications)
                .Select(j => MapToDto(j))
                .ToListAsync();
        }

        public async Task<IEnumerable<JobResponseDTO>> GetFilteredJobsAsync(JobFilter filter)
        {
            var query = _provider.Jobs
                .Include(j => j.HiringManager)
                .Include(j => j.Applications)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filter.Title))
                query = query.Where(j => j.Title.Contains(filter.Title));

            if (!string.IsNullOrEmpty(filter.Location))
                query = query.Where(j => j.Location != null && j.Location.Contains(filter.Location));

            if (filter.SalaryMin.HasValue)
                query = query.Where(j => j.SalaryMin >= filter.SalaryMin || j.SalaryMax >= filter.SalaryMin);

            if (filter.SalaryMax.HasValue)
                query = query.Where(j => j.SalaryMax <= filter.SalaryMax || j.SalaryMin <= filter.SalaryMax);

            if (filter.PostedDate.HasValue)
                query = query.Where(j => j.PostedDate >= filter.PostedDate);
            
            if (filter.ClosingDate.HasValue)
                query = query.Where(j => j.ClosingDate >= filter.ClosingDate);

            if (filter.Status.HasValue)
                query = query.Where(j => j.Status == filter.Status);
            
            return await query
                .Select(j => MapToDto(j))
                .ToListAsync();
        }

        public async Task<JobResponseDTO?> GetJobByIdAsync(int id)
        {
            var job = await _provider.Jobs
                .Include(j => j.HiringManager)
                .Include(j => j.Applications)
                .FirstOrDefaultAsync(j => j.JobId == id);

            return job != null ? MapToDto(job) : null;
        }

        public async Task<JobResponseDTO> CreateJobAsync(CreateJobDto createJobDto)
        {
            var job = new Job
            {
                Title = createJobDto.Title,
                Location = createJobDto.Location,
                SalaryMin = createJobDto.SalaryMin,
                SalaryMax = createJobDto.SalaryMax,
                PostedDate = DateTime.UtcNow,
                ClosingDate = createJobDto.ClosingDate,
                Status = JobStatus.Open,
                HiringManagerId = createJobDto.HiringManagerId
            };

            _provider.Jobs.Add(job);
            await _provider.SaveChangesAsync();

            return await GetJobByIdAsync(job.JobId) ?? throw new InvalidOperationException("Failed to retrieve created job");
        }

        public async Task<JobResponseDTO?> UpdateJobAsync(int id, UpdateJobDTO updateJobDto)
        {
            var job = await _provider.Jobs.FindAsync(id);
            if (job == null)
                return null;

            job.Title = updateJobDto.Title;
            job.Location = updateJobDto.Location;
            job.SalaryMin = updateJobDto.SalaryMin;
            job.SalaryMax = updateJobDto.SalaryMax;
            job.ClosingDate = updateJobDto.ClosingDate;
            job.Status = updateJobDto.Status;
            job.HiringManagerId = updateJobDto.HiringManagerId;

            await _provider.SaveChangesAsync();
            return await GetJobByIdAsync(id);
        }

        public async Task<bool> DeleteJobAsync(int id)
        {
            var job = await _provider.Jobs.FindAsync(id);
            if (job == null)
                return false;

            _provider.Jobs.Remove(job);
            await _provider.SaveChangesAsync();
            return true;
        }

        public async Task<bool> JobExistsAsync(int id)
        {
            return await _provider.Jobs.AnyAsync(j => j.JobId == id);
        }

        public async Task<IEnumerable<JobResponseDTO>> GetJobsByHiringManagerAsync(int hiringManagerId)
        {
            return await _provider.Jobs
                .Include(j => j.HiringManager)
                .Include(j => j.Applications)
                .Where(j => j.HiringManagerId == hiringManagerId)
                .Select(j => MapToDto(j))
                .ToListAsync();
        }

        public async Task<IEnumerable<JobResponseDTO>> GetActiveJobsAsync()
        {
            return await _provider.Jobs
                .Include(j => j.HiringManager)
                .Include(j => j.Applications)
                .Where(j => j.Status == JobStatus.Open && (j.ClosingDate == null || j.ClosingDate > DateTime.UtcNow))
                .Select(j => MapToDto(j))
                .ToListAsync();
        }

        public async Task<bool> UpdateJobStatusAsync(int id, JobStatus status)
        {
            var job = await _provider.Jobs.FindAsync(id);
            if (job == null)
                return false;

            job.Status = status;
            await _provider.SaveChangesAsync();
            return true;
        }

        private static JobResponseDTO MapToDto(Job job)
        {
            return new JobResponseDTO
            {
                JobId = job.JobId,
                Title = job.Title,
                Location = job.Location,
                SalaryMin = job.SalaryMin,
                SalaryMax = job.SalaryMax,
                PostedDate = job.PostedDate,
                ClosingDate = job.ClosingDate,
                Status = job.Status,
                HiringManagerId = job.HiringManagerId,
                HiringManagerName = job.HiringManager?.FirstName + " " + job.HiringManager?.LastName,
                HiringManagerEmail = job.HiringManager?.Email ?? string.Empty,
                ApplicationCount = job.Applications?.Count ?? 0
            };
        }
    }
}