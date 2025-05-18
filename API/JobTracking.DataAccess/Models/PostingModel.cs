using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobApplication.Models
{
    public enum JobPostingStatus
    {
        Draft,
        Open,
        Filled,
        OnHold,
        Closed,
        Expired,
        Cancelled
    }
    
    public class JobPosting
    {
        [Key]
        public int JobPostingId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        
        
        [StringLength(500)]
        public string Qualifications { get; set; }
        
        [StringLength(500)]
        public string Responsibilities { get; set; }
        
        [Required]
        public DateTime PostedDate { get; set; }
        
        public DateTime? ExpiryDate { get; set; }
        
        [Required]
        public JobPostingStatus Status { get; set; }
        
        [StringLength(100)]
        public string Location { get; set; }
        
        public bool IsRemote { get; set; }
        
        [StringLength(50)]
        public string EmploymentType { get; set; } 
        
        [Range(0, int.MaxValue)]
        public int? MinSalary { get; set; }
        
        [Range(0, int.MaxValue)]
        public int? MaxSalary { get; set; }
        
        [StringLength(20)]
        public string SalaryCurrency { get; set; }
        
        public bool IsSalaryNegotiable { get; set; }
        
        [ForeignKey("CreatedBy")]
        public int CreatedById { get; set; }
        public virtual User CreatedBy { get; set; }
        
        [ForeignKey("HiringManager")]
        public int? HiringManagerId { get; set; }
        public virtual User HiringManager { get; set; }
        
        public int NumberOfOpenings { get; set; }
        
        [StringLength(100)]
        public string SkillsRequired { get; set; }
        
        [StringLength(100)]
        public string ExperienceRequired { get; set; }
        
        [StringLength(100)]
        public string EducationRequired { get; set; }
        
        public virtual ICollection<Application> Applications { get; set; }
    }
}