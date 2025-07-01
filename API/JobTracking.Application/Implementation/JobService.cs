using JobTracking.Application.Contracts;
using JobTracking.Application.Contracts.Base;
using JobTracking.Domain.DTOs;
using JobTracking.Domain.Filters;
using JobTracking.Models;
using Microsoft.EntityFrameworkCore;
using JobStatus = JobTracking.Models.JobStatus;

namespace JobTracking.Application.Implementation;

public class JobService : IJobService
{
    private readonly DependencyProvider _provider;

    public JobService(DependencyProvider provider)
    {
        _provider = provider;
    }

    public async Task<IEnumerable<JobResponseDTO>> GetAllJobsAsync()
    {
        var jobs = await _provider.Db.Jobs
            .Include(j => j.HiringManager)
            .Include(j => j.Applications)
            .ToListAsync();

        return jobs.Select(MapToResponseDto).ToList();
    }

    public async Task<IEnumerable<JobResponseDTO>> GetFilteredJobsAsync(JobFilter filter)
    {
        var query = _provider.Db.Jobs
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

        var jobs = await query.ToListAsync();
        return jobs.Select(MapToResponseDto).ToList();
    }

    public async Task<JobResponseDTO?> GetJobByIdAsync(int id)
    {
        var job = await _provider.Db.Jobs
            .Include(j => j.HiringManager)
            .Include(j => j.Applications)
            .FirstOrDefaultAsync(j => j.JobId == id);

        return job != null ? MapToResponseDto(job) : null;
    }

    public async Task<JobResponseDTO> CreateJobAsync(UpdateJobDTO createJobDto)
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

        _provider.Db.Jobs.Add(job);
        await _provider.Db.SaveChangesAsync();

        return await GetJobByIdAsync(job.JobId) ?? throw new InvalidOperationException("Failed to retrieve created job");
    }

    public async Task<JobResponseDTO?> UpdateJobAsync(int id, UpdateJobDTO updateJobDto)
    {
        var job = await _provider.Db.Jobs.FindAsync(id);
        if (job == null)
            return null;

        job.Title = updateJobDto.Title;
        job.Location = updateJobDto.Location;
        job.SalaryMin = updateJobDto.SalaryMin;
        job.SalaryMax = updateJobDto.SalaryMax;
        job.ClosingDate = updateJobDto.ClosingDate;
        job.Status = updateJobDto.Status;
        job.HiringManagerId = updateJobDto.HiringManagerId;

        await _provider.Db.SaveChangesAsync();
        return await GetJobByIdAsync(id);
    }

    public async Task<bool> DeleteJobAsync(int id)
    {
        var job = await _provider.Db.Jobs.FindAsync(id);
        if (job == null)
            return false;

        _provider.Db.Jobs.Remove(job);
        await _provider.Db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> JobExistsAsync(int id)
    {
        return await _provider.Db.Jobs.AnyAsync(j => j.JobId == id);
    }

    public async Task<IEnumerable<JobResponseDTO>> GetJobsByHiringManagerAsync(int hiringManagerId)
    {
        var jobs = await _provider.Db.Jobs
            .Include(j => j.HiringManager)
            .Include(j => j.Applications)
            .Where(j => j.HiringManagerId == hiringManagerId)
            .ToListAsync();

        return jobs.Select(MapToResponseDto).ToList();
    }

    public async Task<IEnumerable<JobResponseDTO>> GetActiveJobsAsync()
    {
        var jobs = await _provider.Db.Jobs
            .Include(j => j.HiringManager)
            .Include(j => j.Applications)
            .Where(j => j.Status == JobStatus.Open && (j.ClosingDate == null || j.ClosingDate > DateTime.UtcNow))
            .ToListAsync();

        return jobs.Select(MapToResponseDto).ToList();
    }

    public async Task<bool> UpdateJobStatusAsync(int id, JobTracking.Domain.Enums.JobStatus status)
    {
        var job = await _provider.Db.Jobs.FindAsync(id);
        if (job == null)
            return false;

        job.Status = (JobStatus)status;
        await _provider.Db.SaveChangesAsync();
        return true;
    }

    private JobResponseDTO MapToResponseDto(Job job)
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