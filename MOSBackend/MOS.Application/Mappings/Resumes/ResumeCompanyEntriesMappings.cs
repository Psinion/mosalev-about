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
            Company = entity.Company,
            WebSiteUrl = entity.WebSiteUrl,
            Description = entity.Description,
        };
    }
}