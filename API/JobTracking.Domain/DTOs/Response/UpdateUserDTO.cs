using System.ComponentModel.DataAnnotations;
using JobTracking.Models;

namespace JobTracking.Domain.DTOs.Response
{
    public class UpdateUserDTO
    {
        [MaxLength(50)]
        public string? FirstName { get; set; }

        [MaxLength(50)]
        public string? LastName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        [MaxLength(100)]
        public string? Address { get; set; }

        [MaxLength(50)]
        public string? City { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? Education { get; set; }

        public string? WorkExperience { get; set; }

        public UserRole? Role { get; set; }
    }
}