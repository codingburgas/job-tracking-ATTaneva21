using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobApplication.Models
{
    
    public class CandidateEducation
    {
        [Key]
        public int EducationId { get; set; }
        
        [Required]
        [ForeignKey("Candidate")]
        public int CandidateId { get; set; }
        public virtual Candidate Candidate { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Institution { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Degree { get; set; }
        
        [StringLength(100)]
        public string FieldOfStudy { get; set; }
        
        public DateTime? StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }
        
        public bool IsCurrentlyStudying { get; set; }
        
        [StringLength(50)]
        public string Grade { get; set; }
        
        [StringLength(500)]
        public string Activities { get; set; }
        
        [StringLength(500)]
        public string Description { get; set; }
        
        [StringLength(100)]
        public string Location { get; set; }
    }
}