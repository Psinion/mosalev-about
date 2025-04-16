using MOS.Application.Data.Repositories;
using MOS.Application.Data.Repositories.Resumes;
using MOS.Application.Data.Services.Resumes;
using MOS.Application.DTOs.Resumes.Requests;
using MOS.Application.DTOs.Resumes.Responses;
using MOS.Application.Mappings.Resumes;
using MOS.Application.OperationResults;
using MOS.Domain.Entities.Resumes;

namespace MOS.Data.EF.Access.Services.Resumes;

public class ResumeCompanyEntriesService : IResumeCompanyEntriesService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IResumeCompanyEntriesRepository resumeCompanyEntriesRepository;
    
    public ResumeCompanyEntriesService(IUnitOfWork unitOfWork, IResumeCompanyEntriesRepository resumeCompanyEntriesRepository)
    {
        this.unitOfWork = unitOfWork;
        this.resumeCompanyEntriesRepository = resumeCompanyEntriesRepository;
    }

    public async Task<OperationResult<ResumeCompanyEntryResponseDto>> CreateResumeSkillAsync(ResumeCompanyEntryCreateRequestDto resumeCompanyEntryRequest)
    {
        var resumeCompanyEntry = new ResumeCompanyEntry()
        {
            ResumeId = resumeCompanyEntryRequest.ResumeId,
            Company = resumeCompanyEntryRequest.Company,
            Description = resumeCompanyEntryRequest.Description,
            WebSiteUrl = resumeCompanyEntryRequest.WebSiteUrl,
        };

        var createdEntity = await resumeCompanyEntriesRepository.CreateAsync(resumeCompanyEntry);
        await unitOfWork.SaveChangesAsync();

        return createdEntity.ToDto();
    }

    public async Task<OperationResult<ResumeCompanyEntryResponseDto>> UpdateResumeCompanyEntryAsync(ResumeCompanyEntryUpdateRequestDto resumeCompanyEntryRequest)
    {
        var resumeCompanyEntry = await resumeCompanyEntriesRepository.GetByIdAsync(resumeCompanyEntryRequest.Id);
        if (resumeCompanyEntry == null)
        {
            return OperationError.NotFound();
        }

        resumeCompanyEntry.Company = resumeCompanyEntryRequest.Company;
        resumeCompanyEntry.Description = resumeCompanyEntryRequest.Description;
        resumeCompanyEntry.WebSiteUrl = resumeCompanyEntryRequest.WebSiteUrl;

        await resumeCompanyEntriesRepository.UpdateAsync(resumeCompanyEntry);
        await unitOfWork.SaveChangesAsync();
        
        return resumeCompanyEntry.ToDto();
    }

    public async Task<OperationResult<bool>> DeleteResumeCompanyEntryAsync(long companyId)
    {
        try
        {
            await resumeCompanyEntriesRepository.DeleteAsync(companyId);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            return OperationError.NotFound();
        }
    }

    public void Dispose()
    {
        resumeCompanyEntriesRepository.Dispose();
    }
}