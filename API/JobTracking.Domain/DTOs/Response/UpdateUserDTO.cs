using JobTracking.Models;
using System.ComponentModel.DataAnnotations;

public class UpdateUserDto
{
    [Required] [StringLength(50)] public string FirstName { get; set; } = string.Empty;

    [Required] [StringLength(50)] public string LastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; } = string.Empty;

    [Phone] [StringLength(20)] public string? PhoneNumber { get; set; }

    [StringLength(200)] public string? Address { get; set; }

    [StringLength(50)] public string? City { get; set; }

    public DateTime DateOfBirth { get; set; }

    [StringLength(1000)] public string? Education { get; set; }

    [StringLength(2000)] public string? WorkExperience { get; set; }

    public UserRole Role { get; set; }
}