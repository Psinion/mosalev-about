using MOS.Application.DTOs.Resumes.Requests;
using MOS.Application.DTOs.Resumes.Responses;
using MOS.Domain.Entities.Resumes;

namespace MOS.Application.Mappings.Resumes;


public static class ResumePostsMappings
{
    public static ResumePostResponseDto ToDto(this ResumePost entity)
    {
        return new ResumePostResponseDto()
        {
            Id = entity.Id,
            ResumeCompanyEntryId = entity.ResumeCompanyEntryId,
            Name = entity.Name,
            Description = entity.Description,
            DateStart = entity.DateStart,
            DateEnd = entity.DateEnd,
        };
    }
    
    public static List<ResumePostResponseDto> ToDtoList(this IList<ResumePost> entities)
    {
        var dtoList = new List<ResumePostResponseDto>(entities.Count());
        foreach (var entity in entities)
        {
            dtoList.Add(entity.ToDto());
        }
        return dtoList;
    }
}