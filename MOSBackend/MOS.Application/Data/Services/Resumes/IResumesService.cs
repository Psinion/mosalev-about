using MOS.Application.DTOs.Resumes.Requests;
using MOS.Application.DTOs.Resumes.Responses;
using MOS.Application.OperationResults;

namespace MOS.Application.Data.Services.Resumes;

public interface IResumesService : IDisposable
{
    Task<OperationResult<ResumeResponseDto>> GetResumeAsync(long resumeId);
    
    Task<OperationResult<ResumeResponseDto>> CreateResumeAsync(ResumeCreateRequestDto resumeRequest);
    
    Task<OperationResult<ResumeResponseDto>> UpdateResumeAsync(ResumeUpdateRequestDto resumeRequest);

    Task<OperationResult<bool>> HasPinnedResumeAsync();
    
    Task<OperationResult<ResumeResponseDto>> GetPinnedResumeAsync();
    
    Task<OperationResult<bool>> PinResumeAsync(long resumeId, bool pinning);
    
    Task<OperationResult<List<ResumeResponseCompactDto>>> GetCompactResumesListAsync();
}