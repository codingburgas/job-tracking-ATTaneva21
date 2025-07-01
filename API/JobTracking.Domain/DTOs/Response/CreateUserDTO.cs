using System.ComponentModel.DataAnnotations;
using JobTracking.Domain.Enums;

namespace JobTracking.Domain.DTOs.Response
{
    public class CreateUserDTO
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(8)]
        public string Password { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        [MaxLength(100)]
        public string? Address { get; set; }

        [MaxLength(50)]
        public string? City { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string? Education { get; set; }

        public string? WorkExperience { get; set; }

        [Required]
        public UserRole Role { get; set; } = UserRole.Candidate;
    }
}