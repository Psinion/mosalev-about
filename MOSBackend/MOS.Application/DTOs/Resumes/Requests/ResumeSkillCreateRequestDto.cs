using System.ComponentModel.DataAnnotations;
using MOS.Domain.Enums;

namespace MOS.Application.DTOs.Resumes.Requests;

public record ResumeSkillCreateRequestDto
{
    [Required] public long ResumeId { get; set; }

    [Required] [StringLength(50)] public string Name { get; set; } = "";

    [Required] public SkillLevelType Level { get; set; }
}