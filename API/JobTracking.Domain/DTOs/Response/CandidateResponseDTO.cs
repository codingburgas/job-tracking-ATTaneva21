using System.ComponentModel.DataAnnotations;

namespace JobTracking.Domain.DTOs;

public class CandidateResponseDTO : UserResponseDTO
{
    public string Address { get; set; }
    public string City { get; set; }
    public string ZipCode { get; set; }
    public string Country { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Education { get; set; }
    public string WorkExperience { get; set; }
}