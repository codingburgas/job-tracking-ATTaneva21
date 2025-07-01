using System.ComponentModel.DataAnnotations;
using JobTracking.Models;

namespace JobTracking.Domain.DTOs;

public class UpdateJobDTO
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Location { get; set; }
    public decimal? SalaryMin { get; set; }
    public decimal? SalaryMax { get; set; }
    public DateTime? ClosingDate { get; set; }
    public JobStatus Status { get; set; }
    public int? HiringManagerId { get; set; }
}
