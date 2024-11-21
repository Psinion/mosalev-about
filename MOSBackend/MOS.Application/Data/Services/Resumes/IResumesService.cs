using MOS.Application.DTOs.Resumes.Requests;
using MOS.Application.DTOs.Resumes.Responses;
using MOS.Application.OperationResults;
using MOS.Domain.Enums;

namespace MOS.Application.Data.Services.Resumes;

public interface IResumesService : IDisposable
{
    Task<OperationResult<ResumeResponseDto>> GetResumeAsync(long resumeId);
    
    Task<OperationResult<ResumeResponseDto>> CreateResumeAsync(ResumeCreateRequestDto resumeRequest);
    
    Task<OperationResult<bool>> PinResumeAsync(long resumeId, Locale locale);
    
    Task<OperationResult<List<ResumeResponseCompactDto>>> GetCompactResumesListAsync();
}