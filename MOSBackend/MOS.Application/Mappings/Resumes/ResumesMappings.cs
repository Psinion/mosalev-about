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
            DateCreate = entity.DateCreate,
            DateUpdate = entity.DateUpdate,
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
            DateCreate = entity.DateCreate,
            UserCreate = entity.UserCreate?.ToDto(),
            DateUpdate = entity.DateUpdate,
            UserUpdate = entity.UserUpdate?.ToDto()
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