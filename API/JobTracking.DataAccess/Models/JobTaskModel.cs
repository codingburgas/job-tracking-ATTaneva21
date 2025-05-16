
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobTracking.Models
{
    public enum TaskStatus
    {
        Todo,
        InProgress,
        Reviewing,
        Completed
    }
    
    public class JobTaskModel
    {
        [Key]
        public int TaskId { get; set; }
        
        [Required]
        [ForeignKey("Job")]
        public int JobId { get; set; }
        public virtual Job Job { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        
        
        [Required]
        public DateTime CreatedDate { get; set; }
        
        public DateTime? DueDate { get; set; }
        
        public DateTime? CompletedDate { get; set; }
        
        [Required]
        public TaskStatus Status { get; set; }
        
        [ForeignKey("AssignedTo")]
        public int? AssignedToId { get; set; }
        public virtual User AssignedTo { get; set; }
        
    }
}