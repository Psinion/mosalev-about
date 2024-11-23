using MOS.Application.DTOs.Resumes.Responses;
using MOS.Domain.Entities.Resumes;

namespace MOS.Application.Mappings.Resumes;


public static class ResumeMappings
{
    public static ResumeResponseDto ToDto(this Resume entity)
    {
        return new ResumeResponseDto()
        {
            Id = entity.Id,
            Title = entity.Title,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email,
            Salary = entity.Salary,
        };
    }
    
    public static ResumeResponseCompactDto ToCompactDto(this Resume entity)
    {
        return new ResumeResponseCompactDto()
        {
            Id = entity.Id,
            Title = entity.Title,
            PinnedToLocale = entity.PinnedToLocale
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