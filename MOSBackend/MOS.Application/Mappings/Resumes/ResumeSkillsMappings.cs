using MOS.Application.DTOs.Resumes.Responses;
using MOS.Domain.Entities.Resumes;

namespace MOS.Application.Mappings.Resumes;

public static class ResumeSkillsMappings
{
    public static ResumeSkillResponseDto ToDto(this ResumeSkill entity)
    {
        return new ResumeSkillResponseDto()
        {
            Id = entity.Id,
            ResumeId = entity.ResumeId,
            Name = entity.Name,
            Level = entity.Level,
        };
    }
    
    public static List<ResumeSkillResponseDto> ToDtoList(this IList<ResumeSkill> entities)
    {
        var dtoList = new List<ResumeSkillResponseDto>(entities.Count());
        foreach (var entity in entities)
        {
            dtoList.Add(entity.ToDto());
        }
        return dtoList;
    }
}