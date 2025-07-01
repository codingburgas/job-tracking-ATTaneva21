using System.ComponentModel.DataAnnotations;

namespace JobTracking.Domain.DTOs.Response
{
    public class CreateJobDTO
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        public string Status { get; set; } = "Open";
    }
}