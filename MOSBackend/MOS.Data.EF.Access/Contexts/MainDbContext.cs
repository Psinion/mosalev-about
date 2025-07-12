using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MOS.Application.Data.Services.Users;
using MOS.Data.EF.Access.Contexts.Extensions;
using MOS.Domain.Entities;
using MOS.Domain.Entities.Files;
using MOS.Domain.Entities.Projects;
using MOS.Domain.Entities.Resumes;
using MOS.Domain.Entities.Users;

namespace MOS.Data.EF.Access.Contexts;

public class MainDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    
    public DbSet<Resume> Resumes { get; set; }
    public DbSet<ResumeCompanyEntry> ResumeCompanyEntries { get; set; }
    public DbSet<ResumeCourse> ResumeCourses { get; set; }
    public DbSet<ResumeEducation> ResumeEducations { get; set; }
    public DbSet<ResumePost> ResumePosts { get; set; }
    public DbSet<ResumeSkill> ResumeSkills { get; set; }
    
    public DbSet<Project> Projects { get; set; }
    public DbSet<Article> Articles { get; set; }
    
    public DbSet<UploadedFile> UploadedFiles { get; set; }
    
    public MainDbContext(DbContextOptions<MainDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes()
                     .Where(e => e.ClrType.IsAssignableTo(typeof(IAuditableEntity<int>))))
        {
            //entityType.ApplyAuditableConfig<int>(modelBuilder);
        }
    }
}