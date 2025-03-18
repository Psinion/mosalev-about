namespace MOS.Application.DTOs.Resumes.Responses;

public record ResumeCompanyEntryResponseDto
{
    public long Id { get; set; }

    public long ResumeId { get; set; }
    
    public string Company { get; set; } = "";
    
    public string? WebSiteUrl { get; set; }    
    
    public string? Description { get; set; }
    
    public List<ResumePostResponseDto> ResumePosts { get; set; } = new();
}