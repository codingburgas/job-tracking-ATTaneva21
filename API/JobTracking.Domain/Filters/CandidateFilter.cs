namespace JobTracking.Domain.Filters;

public class CandidateFilter
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public bool Experience { get; set; }
}