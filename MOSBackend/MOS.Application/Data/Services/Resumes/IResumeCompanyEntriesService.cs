using MOS.Application.DTOs.Resumes.Requests;
using MOS.Application.DTOs.Resumes.Responses;
using MOS.Application.OperationResults;

namespace MOS.Application.Data.Services.Resumes;

public interface IResumeCompanyEntriesService : IDisposable
{
    Task<OperationResult<ResumeCompanyEntryResponseDto>> CreateResumeCompanyEntryAsync(ResumeCompanyEntryCreateRequestDto resumeCompanyEntryRequest);
    
    Task<OperationResult<ResumeCompanyEntryResponseDto>> UpdateResumeCompanyEntryAsync(ResumeCompanyEntryUpdateRequestDto resumeCompanyEntryRequest);
}