namespace MOS.Domain.Entities.Resumes;

public class ResumeCompanyEntry : Entity
{
    public Company Company { get; set; }
    
    public DateTime DateStart { get; set; }
    public DateTime? DateEnd { get; set; }
    public string? Description { get; set; }

    public List<ResumePost> ResumePosts { get; set; } = new();
}