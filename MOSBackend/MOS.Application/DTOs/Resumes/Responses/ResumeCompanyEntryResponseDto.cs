namespace MOS.Application.DTOs.Resumes.Responses;

public class ResumeCompanyEntryResponseDto
{
    public long Id { get; set; }

    public long ResumeId { get; set; }
    
    public string Company { get; set; } = "";
    
    public string? WebSiteUrl { get; set; }    
    
    public string? Description { get; set; }
}