using MOS.Application.DTOs.Resumes.Requests;
using MOS.Application.DTOs.Resumes.Responses;
using MOS.Application.OperationResults;

namespace MOS.Application.Data.Services.Resumes;

public interface IResumesService : IDisposable
{
    Task<OperationResult<ResumeResponseDto>> CreateResumeAsync(ResumeCreateRequestDto resumeRequest);
    
    Task<OperationResult<List<ResumeResponseCompactDto>>> GetCompactResumesListAsync();
}