using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobTracking.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }
        
        [StringLength(20)]
        public string Phone { get; set; }
        
        public virtual ICollection<Job> Jobs { get; set; }
    }
}