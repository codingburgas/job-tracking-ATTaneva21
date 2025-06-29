using JobTracking.Domain.Filters.Base;
using JobTracking.Models;

namespace JobTracking.Domain.Filters
{
    public class JobFilter : BaseFilter<Job>
    {
        public string? Title { get; set; }
        public string? Location { get; set; }
        public decimal? SalaryMin { get; set; }
        public decimal? SalaryMax { get; set; }
        public DateTime? PostedDate { get; set; }
        public DateTime? ClosingDate { get; set; }
        public JobStatus? Status { get; set; }
    }
}