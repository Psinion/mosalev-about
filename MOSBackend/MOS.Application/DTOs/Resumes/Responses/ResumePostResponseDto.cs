namespace MOS.Application.DTOs.Resumes.Responses;

public class ResumePostResponseDto
{
    public long Id { get; set; }

    public long ResumeCompanyEntryId { get; set; }
    
    public string Name { get; set; } = "";
    
    public string? Description { get; set; }
    
    public DateOnly DateStart { get; set; }
    public DateOnly? DateEnd { get; set; }
}