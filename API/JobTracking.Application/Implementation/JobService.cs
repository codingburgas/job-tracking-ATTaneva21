using JobTracking.Application.Contracts;
using JobTracking.Application.Contracts.Base;
using JobTracking.Domain.DTOs;
using JobTracking.Domain.Enums;
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
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Filter?.TitleContains))
        {
            var title = filter.Filter.TitleContains;
            query = query.Where(j => j.Title.Contains(title));
        }

        if (filter.Filter?.Status is Domain.Enums.JobStatus status)
        {
            query = query.Where(j => j.Status == (JobStatus)status);
        }

        if (filter.Filter?.HiringManagerId is int managerId)
        {
            query = query.Where(j => j.HiringManagerId == managerId);
        }

        if (!string.IsNullOrEmpty(filter.SortBy))
        {
            query = filter.SortDirection == SortOrderEnum.Desc
                ? query.OrderByDescending(j => EF.Property<object>(j, filter.SortBy))
                : query.OrderBy(j => EF.Property<object>(j, filter.SortBy));
        }

        query = query
            .Skip((filter.Page - 1) * filter.PageSize)
            .Take(filter.PageSize);

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
        job.Status = (JobStatus)updateJobDto.Status;
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

    public async Task<bool> UpdateJobStatusAsync(int id, Domain.Enums.JobStatus status)
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
            Status = (Domain.Enums.JobStatus)(int)job.Status,
            HiringManagerId = job.HiringManagerId,
            HiringManagerName = job.HiringManager?.FirstName + " " + job.HiringManager?.LastName,
            HiringManagerEmail = job.HiringManager?.Email ?? string.Empty,
            ApplicationCount = job.Applications?.Count ?? 0
        };
    }
}
