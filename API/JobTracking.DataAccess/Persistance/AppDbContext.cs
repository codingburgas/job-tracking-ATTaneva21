using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using JobTracking.Models;
using Microsoft.AspNetCore.Identity;

namespace JobTracking.DataAccess
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Job> Jobs { get; set; }
        public DbSet<Application> JobApplications { get; set; }
        public DbSet<Candidate> Candidates { get; set; }

        
    }
}