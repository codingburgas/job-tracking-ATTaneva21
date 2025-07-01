using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobTracking.Domain.DTOs;

public class ApplicationResponseDTO
{
    [Key] public int ApplicationId { get; set; }

    [Required] public int JobId { get; set; }

    [ForeignKey("JobId")] public virtual JobResponseDTO Job { get; set; }

    [Required] public int UserId { get; set; }

    [ForeignKey("UserId")] public virtual UserResponseDTO User { get; set; }

    public DateTime ApplicationDate { get; set; }

    [StringLength(1000)] public string AdditionalInfo { get; set; }

    public DateTime? LastUpdated { get; set; }
}