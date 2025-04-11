namespace MOS.Domain.Entities.Resumes;

public class ResumeCompanyEntry : Entity<long>
{
    public long ResumeId { get; set; }
    public string Company { get; set; } = "";
    public string? WebSiteUrl { get; set; }
    public string? Description { get; set; }

    public List<ResumePost> ResumePosts { get; set; } = new();
}