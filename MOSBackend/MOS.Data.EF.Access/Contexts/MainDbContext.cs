using Microsoft.EntityFrameworkCore;
using MOS.Domain.Entities.Index;
using MOS.Domain.Entities.Resumes;
using MOS.Domain.Entities.Users;

namespace MOS.Data.EF.Access.Contexts;

public class MainDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    
    public DbSet<IndexContent> IndexContents { get; set; }
    
    public DbSet<Resume> Resumes { get; set; }
    public DbSet<ResumeCompanyEntry> ResumeCompanyEntries { get; set; }
    public DbSet<ResumeCourse> ResumeCourses { get; set; }
    public DbSet<ResumeEducation> ResumeEducations { get; set; }
    public DbSet<ResumePost> ResumePosts { get; set; }
    public DbSet<ResumeSkill> ResumeSkills { get; set; }
    
    public MainDbContext(DbContextOptions<MainDbContext> options)
        : base(options)
    {
    }
}