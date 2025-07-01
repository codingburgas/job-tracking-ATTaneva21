    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace JobTracking.Models
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
            
            [ForeignKey("HiringManagerId")]
            public User HiringManager { get; set; }
            public int HiringManagerId { get; set; }
            
            public virtual ICollection<Application> Applications { get; set; }
        }
    }