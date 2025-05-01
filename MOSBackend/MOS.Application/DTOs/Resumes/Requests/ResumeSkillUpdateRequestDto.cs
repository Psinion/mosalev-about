using System.ComponentModel.DataAnnotations;
using MOS.Domain.Enums;

namespace MOS.Application.DTOs.Resumes.Requests;

public record ResumeSkillUpdateRequestDto
{
    [Required] public int Id { get; set; }

    [Required] [StringLength(50)] public string Name { get; set; } = "";

    [Required] public SkillLevelType Level { get; set; }
}