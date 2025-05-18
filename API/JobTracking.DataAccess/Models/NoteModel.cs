using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobApplication.Models
{
    public class ApplicationNote
    {
        [Key]
        public int NoteId { get; set; }
        
        [Required]
        [ForeignKey("JobApplication")]
        public int ApplicationId { get; set; }
        public virtual JobApplication JobApplication { get; set; }
        
        [Required]
        [ForeignKey("User")]
        public int CreatedById { get; set; }
        public virtual User CreatedBy { get; set; }
        
        [Required]
        public DateTime CreatedDate { get; set; }
        
        [Required]
        [StringLength(1000)]
        public string Content { get; set; }
        
        public bool IsPrivate { get; set; }
    }
}