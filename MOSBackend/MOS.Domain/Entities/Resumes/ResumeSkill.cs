using MOS.Domain.Enums;

namespace MOS.Domain.Entities.Resumes;

public class ResumeSkill : Entity<int>
{
    public int ResumeId { get; set; }
    public string Name { get; set; } = "";
    public SkillLevelType Level { get; set; }
}