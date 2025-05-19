using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobApplication.Models
{
    public class Candidate : User
    {
        
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
        
        public virtual ICollection<Application> Applications { get; set; }
        public virtual ICollection<CandidateEducation> Education { get; set; }
    }
}