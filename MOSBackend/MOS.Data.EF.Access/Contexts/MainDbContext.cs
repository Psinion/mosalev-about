using Microsoft.EntityFrameworkCore;
using MOS.Application.Data.Services.Users;
using MOS.Domain.Entities;
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
    
    public MainDbContext(DbContextOptions<MainDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes()
                     .Where(e => e.ClrType.IsAssignableTo(typeof(IAuditableEntity<long>))))
        {
            modelBuilder.Entity(entityType.ClrType, builder =>
            {
                builder
                    .Property("CreatedAt")
                    .IsRequired()
                    .HasDefaultValueSql("NOW()");
                
                builder
                    .Property("CreatedBy")
                    .IsRequired()
                    .HasDefaultValue(1);
                
                builder
                    .Property("UpdatedBy")
                    .IsRequired(false)
                    .HasDefaultValue(null);
                
                builder
                    .Property("DeletedBy")
                    .IsRequired(false)
                    .HasDefaultValue(null);
                
                builder
                    .HasOne("Creator")
                    .WithMany()
                    .HasForeignKey("CreatedBy")
                    .OnDelete(DeleteBehavior.Restrict);
            
                builder.HasOne("Updater")
                    .WithMany()
                    .HasForeignKey("e.UpdatedBy")
                    .OnDelete(DeleteBehavior.Restrict);
                
                builder.HasOne("Deleter")
                    .WithMany()
                    .HasForeignKey("DeletedBy")
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}