namespace MOS.Application.DTOs.Resumes.Responses;

public record ResumeResponseDto
{
    public long Id { get; set; }

    public string Title { get; set; }
    
    public string? About { get; set; }
    
    public DateTime? DateCreate { get; set; }
    
    public DateTime? DateUpdate { get; set; }

    public List<ResumeCompanyEntryResponseDto> CompanyEntries { get; set; } = new();
}