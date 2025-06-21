namespace JobTracking.Domain.DTOs;
    
public class UserResponseDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? ZipCode { get; set; }
    public string? Country { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public DateTime DateRegistered { get; set; }
    public string? Education { get; set; }
    public string? WorkExperience { get; set; }
    public string Role { get; set; } = string.Empty;
}