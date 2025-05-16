using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobTracking.Models
{

    public enum JobStatus
    {
        Assigned,
        InProgress,
        OnHold,
        Completed,
        Cancelled,
    }
    
    public enum JobPriority
    {
        Low,
        Normal,
        High
    }
    
    public class Job
    {
        [Key]
        public int JobId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        
        [StringLength(500)]
        public string Description { get; set; }
        
        [Required]
        public DateTime CreatedDate { get; set; }
        
        public DateTime? StartDate { get; set; }
        
        public DateTime? DueDate { get; set; }
        
        public DateTime? CompletedDate { get; set; }
        
        [Required]
        public JobStatus Status { get; set; }
        
        [Required]
        public JobPriority Priority { get; set; }

        public decimal? Cost { get; set; }
        
        [ForeignKey("Customer")]
        public int? CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        
        public virtual ICollection<JobTaskModel> Tasks { get; set; }
        
    }
}