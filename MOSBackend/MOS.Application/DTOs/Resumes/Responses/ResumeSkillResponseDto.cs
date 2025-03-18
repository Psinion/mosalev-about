using MOS.Domain.Enums;

namespace MOS.Application.DTOs.Resumes.Responses;

public record ResumeSkillResponseDto
{
    public long Id { get; set; }

    public long ResumeId { get; set; }
    
    public string Name { get; set; } = "";
    
    public SkillLevelType Level { get; set; }
}