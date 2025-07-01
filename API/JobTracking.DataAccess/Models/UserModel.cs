using JobTracking.Domain.Enums;
using JobTracking.Models;
using Microsoft.AspNetCore.Identity;

public class User : IdentityUser<int>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public string? Address { get; set; }
    public string? City { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime DateRegistered { get; set; } = DateTime.UtcNow;
    public string? Education { get; set; }
    public string? WorkExperience { get; set; }
    public UserRole Role { get; set; } = UserRole.Candidate;

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();
    public virtual ICollection<Job> ManagedJobs { get; set; } = new List<Job>();
}