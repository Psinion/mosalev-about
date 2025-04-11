using MOS.Application.DTOs.Resumes.Responses;
using MOS.Application.Mappings.Users;
using MOS.Domain.Entities.Resumes;

namespace MOS.Application.Mappings.Resumes;


public static class ResumesMappings
{
    public static ResumeResponseDto ToDto(this Resume entity)
    {
        return new ResumeResponseDto()
        {
            Id = entity.Id,
            Title = entity.Title,
            About = entity.About,
            DateCreate = entity.CreatedAt,
            DateUpdate = entity.UpdatedAt,
            CompanyEntries = entity.CompanyEntries.ToDtoList(),
            Skills = entity.Skills.ToDtoList(),
        };
    }
    
    public static ResumeResponseCompactDto ToCompactDto(this Resume entity)
    {
        return new ResumeResponseCompactDto()
        {
            Id = entity.Id,
            Title = entity.Title,
            PinnedToLocale = entity.PinnedToLocale,
            DateCreate = entity.CreatedAt,
            UserCreate = entity.Creator.ToDto(),
            DateUpdate = entity.UpdatedAt,
            UserUpdate = entity.Updater?.ToDto()
        };
    }

    public static List<ResumeResponseCompactDto> ToCompactDtoList(this IList<Resume> entities)
    {
        var dtoList = new List<ResumeResponseCompactDto>(entities.Count());
        foreach (var entity in entities)
        {
            dtoList.Add(entity.ToCompactDto());
        }
        return dtoList;
    }
}