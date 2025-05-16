using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobTracking.Models
{
    public enum UserRole
    {
        Administrator,
        Manager,
        User
    }
    
    public class User
    {
        [Key]
        public int UserId { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Username { get; set; }
        
        [Required]
        [StringLength(100)]
        public string FullName { get; set; }
        
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        public UserRole Role { get; set; }

        public virtual ICollection<JobTaskModel> Assignments { get; set; }
    }
}