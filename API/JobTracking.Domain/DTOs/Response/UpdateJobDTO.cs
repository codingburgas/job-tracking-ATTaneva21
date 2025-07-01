using System.ComponentModel.DataAnnotations;
using JobStatus = JobTracking.Models.JobStatus;

namespace JobTracking.Domain.DTOs;

public class UpdateJobDTO
{
    [Required] [StringLength(100)] public string Title { get; set; }

    [StringLength(100)] public string? Location { get; set; }

    [Range(0, double.MaxValue)] public decimal? SalaryMin { get; set; }

    [Range(0, double.MaxValue)] public decimal? SalaryMax { get; set; }

    public DateTime? ClosingDate { get; set; }

    [Required] public JobStatus Status { get; set; }

    public int? HiringManagerId { get; set; }
}