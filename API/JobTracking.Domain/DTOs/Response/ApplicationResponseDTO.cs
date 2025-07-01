using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobTracking.Domain.DTOs;

public class ApplicationResponseDTO
{
    public int Id { get; set; }
    public int JobId { get; set; }
    public int CandidateId { get; set; }
    public DateTime AppliedOn { get; set; }
    public string? Notes { get; set; }
}
