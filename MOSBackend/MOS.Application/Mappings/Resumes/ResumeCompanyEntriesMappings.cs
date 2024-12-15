using MOS.Application.DTOs.Resumes.Requests;
using MOS.Application.DTOs.Resumes.Responses;
using MOS.Domain.Entities.Resumes;

namespace MOS.Application.Mappings.Resumes;


public static class ResumeCompanyEntriesMappings
{
    public static ResumeCompanyEntryResponseDto ToDto(this ResumeCompanyEntry entity)
    {
        return new ResumeCompanyEntryResponseDto()
        {
            Id = entity.Id,
            ResumeId = entity.ResumeId,
            Company = entity.Company,
            WebSiteUrl = entity.WebSiteUrl,
            Description = entity.Description,
        };
    }
    
    public static List<ResumeCompanyEntryResponseDto> ToDtoList(this IList<ResumeCompanyEntry> entities)
    {
        var dtoList = new List<ResumeCompanyEntryResponseDto>(entities.Count());
        foreach (var entity in entities)
        {
            dtoList.Add(entity.ToDto());
        }
        return dtoList;
    }
}