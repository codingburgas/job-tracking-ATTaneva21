using System.ComponentModel.DataAnnotations;

namespace JobTracking.Domain.DTOs;
public class UpdateApplicationDTO
{
    [MaxLength(500)]
    public string? Notes { get; set; }

    public DateTime? UpdatedOn { get; set; } = DateTime.UtcNow;
}