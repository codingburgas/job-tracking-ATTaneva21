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

        [MaxLength(100)]
        public string? Location { get; set; }

        public decimal? SalaryMin { get; set; }

        public decimal? SalaryMax { get; set; }

        public DateTime? ClosingDate { get; set; }

        [Required]
        public int HiringManagerId { get; set; }

        [Required]
        public string Status { get; set; } = "Open";
    }
}