// User.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobApplication.Models
{
    public class User
    {
        [Key] public int UserId { get; set; }

        [Required] [StringLength(50)] public string FirstName { get; set; }

        [Required] [StringLength(50)] public string LastName { get; set; }

        [NotMapped] public string FullName => $"{FirstName} {LastName}";

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required] [StringLength(100)] public string PasswordHash { get; set; }

        [Phone] [StringLength(20)] public string PhoneNumber { get; set; }

        [StringLength(100)] public string Address { get; set; }

        [StringLength(50)] public string City { get; set; }

        [StringLength(20)] public string ZipCode { get; set; }

        [StringLength(50)] public string Country { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime DateRegistered { get; set; }

        [StringLength(500)] public string Education { get; set; }

        [StringLength(500)] public string WorkExperience { get; set; }

        public UserRole Role { get; set; } = UserRole.Applicant;

        public virtual ICollection<JobApplication> JobApplications { get; set; }
        public virtual ICollection<Job> ManagedJobs { get; set; }
    }

    public enum UserRole
    {
        Applicant,
        HiringManager,
        Administrator
    }
}