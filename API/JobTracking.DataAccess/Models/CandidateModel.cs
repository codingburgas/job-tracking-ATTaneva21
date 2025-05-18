using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobApplication.Models
{
    public class Candidate
    {
        [Key]
        public int CandidateId { get; set; }
        
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }
        
        [StringLength(20)]
        public string Phone { get; set; }
        
        [StringLength(100)]
        public string Address { get; set; }
        
        [StringLength(50)]
        public string City { get; set; }
        
        [StringLength(20)]
        public string ZipCode { get; set; }
        
        [StringLength(50)]
        public string Country { get; set; }
        
        public DateTime? DateOfBirth { get; set; }
        
        [StringLength(200)]
        public string CurrentEmployer { get; set; }
        
        [StringLength(100)]
        public string CurrentPosition { get; set; }
        
        public int? YearsOfExperience { get; set; }
        
        [StringLength(100)]
        public string HighestEducation { get; set; }
        
        [StringLength(100)]
        public string Field { get; set; }

        [StringLength(100)]
        public string DesiredRole { get; set; }
        
        public decimal? ExpectedSalary { get; set; }
        public bool IsAvailableForRelocate { get; set; }
        public DateTime RegistrationDate { get; set; }
        
        public virtual ICollection<JobApplication> Applications { get; set; }
        public virtual ICollection<CandidateEducation> Education { get; set; }
    }
}