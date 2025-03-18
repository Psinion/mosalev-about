using MOS.Application.DTOs.Resumes.Requests;
using MOS.Application.DTOs.Resumes.Responses;
using MOS.Application.OperationResults;

namespace MOS.Application.Data.Services.Resumes;

public interface IResumeSkillsService : IDisposable
{
    Task<OperationResult<ResumeSkillResponseDto>> CreateResumeSkillAsync(ResumeSkillCreateRequestDto resumeSkillRequest);
    
    Task<OperationResult<ResumeSkillResponseDto>> UpdateResumeSkillAsync(ResumeSkillUpdateRequestDto resumeSkillRequest);
    
    Task<OperationResult<bool>> DeleteResumeSkillAsync(long postId);
}