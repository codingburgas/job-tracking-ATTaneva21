using System.ComponentModel.DataAnnotations;

namespace JobTracking.Domain.DTOs;

public class CreateApplicationDTO
{
    [Required]
    public int JobId { get; set; }

    [Required]
    public int CandidateId { get; set; }

    [Required]
    public DateTime AppliedOn { get; set; } = DateTime.UtcNow;

    [MaxLength(500)]
    public string? Notes { get; set; }
}