using JobTracking.Domain.Enums;

namespace JobTracking.Domain.Filters;

public class JobFilterCriteria
{
    public string? TitleContains { get; set; }
    public JobStatus? Status { get; set; }
    public int? HiringManagerId { get; set; }
}