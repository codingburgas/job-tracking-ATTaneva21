using JobTracking.Domain.Enums;

namespace JobTracking.Domain.DTOs;

public class JobResponseDTO
{
    public int JobId { get; set; }
    public string Title { get; set; }
    public string Location { get; set; }
    public decimal? SalaryMin { get; set; }
    public decimal? SalaryMax { get; set; }
    public DateTime PostedDate { get; set; }
    public DateTime? ClosingDate { get; set; }
    public JobStatus Status { get; set; }
    
    public int? HiringManagerId { get; set; }
    public string HiringManagerName { get; set; }
    public string HiringManagerEmail { get; set; }
    
    public int ApplicationCount { get; set; }
}

