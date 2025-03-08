using MOS.Application.DTOs.Resumes.Requests;
using MOS.Application.DTOs.Resumes.Responses;
using MOS.Application.OperationResults;

namespace MOS.Application.Data.Services.Resumes;

public interface IResumePostsService : IDisposable
{
    Task<OperationResult<ResumePostResponseDto>> CreateResumePostAsync(ResumePostCreateRequestDto resumePostRequest);
    
    Task<OperationResult<ResumePostResponseDto>> UpdateResumePostAsync(ResumePostUpdateRequestDto resumePostRequest);
    
    Task<OperationResult<bool>> DeleteResumePostAsync(long postId);
}