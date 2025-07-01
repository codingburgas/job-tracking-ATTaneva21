using JobTracking.Domain.Enums;

namespace JobTracking.Domain.Filters;

public class UserFilterCriteria
{
    public string? NameContains { get; set; }
    public UserRole? Role { get; set; }
    public string? City { get; set; }
}