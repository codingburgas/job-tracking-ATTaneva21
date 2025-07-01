using System.ComponentModel.DataAnnotations;

namespace JobTracking.Domain.DTOs.Response
{
    public class UpdateUserDTO
    {
        [MaxLength(100)]
        public string? Name { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public string? Role { get; set; }
    }
}