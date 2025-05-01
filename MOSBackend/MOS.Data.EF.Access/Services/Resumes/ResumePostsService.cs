using MOS.Application.Data.Repositories;
using MOS.Application.Data.Repositories.Resumes;
using MOS.Application.Data.Services.Resumes;
using MOS.Application.DTOs.Resumes.Requests;
using MOS.Application.DTOs.Resumes.Responses;
using MOS.Application.Mappings.Resumes;
using MOS.Application.OperationResults;
using MOS.Domain.Entities.Resumes;

namespace MOS.Data.EF.Access.Services.Resumes;

public class ResumePostsService : IResumePostsService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IResumePostsRepository resumePostsRepository;
    
    public ResumePostsService(IUnitOfWork unitOfWork, IResumePostsRepository resumePostsRepository)
    {
        this.unitOfWork = unitOfWork;
        this.resumePostsRepository = resumePostsRepository;
    }

    public async Task<OperationResult<ResumePostResponseDto>> CreateResumePostAsync(ResumePostCreateRequestDto resumePostRequest)
    {
        var resumeCompanyEntry = new ResumePost()
        {
            ResumeCompanyEntryId = resumePostRequest.ResumeCompanyEntryId,
            Name = resumePostRequest.Name,
            Description = resumePostRequest.Description,
            DateStart = resumePostRequest.DateStart,
            DateEnd = resumePostRequest.DateEnd,
        };

        var createdEntity = await resumePostsRepository.CreateAsync(resumeCompanyEntry);
        await unitOfWork.SaveChangesAsync();

        return createdEntity.ToDto();
    }

    public async Task<OperationResult<ResumePostResponseDto>> UpdateResumePostAsync(ResumePostUpdateRequestDto resumePostRequest)
    {
        var resumeCompanyEntry = await resumePostsRepository.GetByIdAsync(resumePostRequest.Id);
        if (resumeCompanyEntry == null)
        {
            return OperationError.NotFound();
        }

        resumeCompanyEntry.Name = resumePostRequest.Name;
        resumeCompanyEntry.Description = resumePostRequest.Description;
        resumeCompanyEntry.DateStart = resumePostRequest.DateStart;
        resumeCompanyEntry.DateEnd = resumePostRequest.DateEnd;

        await resumePostsRepository.UpdateAsync(resumeCompanyEntry);
        await unitOfWork.SaveChangesAsync();
        
        return resumeCompanyEntry.ToDto();
    }

    public async Task<OperationResult<bool>> DeleteResumePostAsync(int postId)
    {
        try
        {
            await resumePostsRepository.DeleteAsync(postId);
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
        resumePostsRepository.Dispose();
    }
}