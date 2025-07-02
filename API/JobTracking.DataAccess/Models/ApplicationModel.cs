using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobTracking.Models
{
    public class Application
    {
        [Key]
        public int ApplicationId { get; set; }
        
        [Required]
        public int JobId { get; set; }
        
        [ForeignKey("JobId")]
        public virtual Job Job { get; set; }
        
        [Required]
        public int UserId { get; set; }
        
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        
        public DateTime ApplicationDate { get; set; }
        
        public DateTime? LastUpdated { get; set; }
    }
}