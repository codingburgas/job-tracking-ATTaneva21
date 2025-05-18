using JobApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace JobTracking.DataAccess;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<Application> JobApplications { get; set; }
    public DbSet<CandidateEducation> Educations { get; set; }
    public DbSet<JobPosting> Postings { get; set; }
    public DbSet<Candidate> Candidates { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=JobTracking;Trusted_Connection=true");
        }
    }

    public override EntityEntry Add(object entity)
    {
        return base.Add(entity);
    }
}