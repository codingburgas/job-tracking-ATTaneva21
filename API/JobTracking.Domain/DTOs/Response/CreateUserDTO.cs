using JobTracking.Models;
using System.ComponentModel.DataAnnotations;

namespace JobTracking.Domain.DTOs;

public class CreateUserDto
{
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(255)]
    public string Password { get; set; } = string.Empty; 

    [Phone]
    [StringLength(20)]
    public string? PhoneNumber { get; set; }

    [StringLength(200)]
    public string? Address { get; set; }

    [StringLength(50)]
    public string? City { get; set; }

    [StringLength(20)]
    public string? ZipCode { get; set; }

    [StringLength(50)]
    public string? Country { get; set; }

    public DateTime DateOfBirth { get; set; }

    [StringLength(1000)]
    public string? Education { get; set; }

    [StringLength(2000)]
    public string? WorkExperience { get; set; }

    public UserRole Role { get; set; } = UserRole.Candidate;
}