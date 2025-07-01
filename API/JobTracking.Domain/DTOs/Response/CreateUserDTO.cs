using System.ComponentModel.DataAnnotations;

namespace JobTracking.Domain.DTOs.Response
{
    public class CreateUserDTO
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = "Candidate"; 
    }
}