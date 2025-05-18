﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobApplication.Models
{
    public enum JobStatus
    {
        Draft,
        Open,
        Filled,
        Closed,
        Cancelled
    }
    
    public class Job
    {
        [Key]
        public int JobId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        
        [StringLength(100)]
        public string Location { get; set; }
        
        [Range(0, double.MaxValue)]
        public decimal? SalaryMin { get; set; }
        
        [Range(0, double.MaxValue)]
        public decimal? SalaryMax { get; set; }
        
        public DateTime PostedDate { get; set; }
        
        public DateTime? ClosingDate { get; set; }
        
        [Required]
        public JobStatus Status { get; set; } = JobStatus.Open;
        
        public int? HiringManagerId { get; set; }
        
        [ForeignKey("HiringManagerId")]
        public virtual User HiringManager { get; set; }
        
        public virtual ICollection<JobApplication> Applications { get; set; }
    }
}