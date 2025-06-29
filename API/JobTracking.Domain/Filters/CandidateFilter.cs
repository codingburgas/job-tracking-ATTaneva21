using JobTracking.Domain.Filters.Base;
using JobTracking.Models;

namespace JobTracking.Domain.Filters
{
    public class CandidateFilter : BaseFilter<Candidate>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public UserRole? Role { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? DateRegisteredFrom { get; set; }
        public DateTime? DateRegisteredTo { get; set; }
    }
}