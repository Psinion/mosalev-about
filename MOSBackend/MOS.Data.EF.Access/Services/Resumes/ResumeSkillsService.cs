using MOS.Application.Data.Repositories;
using MOS.Application.Data.Repositories.Resumes;
using MOS.Application.Data.Services.Resumes;
using MOS.Application.DTOs.Resumes.Requests;
using MOS.Application.DTOs.Resumes.Responses;
using MOS.Application.Mappings.Resumes;
using MOS.Application.OperationResults;
using MOS.Domain.Entities.Resumes;

namespace MOS.Data.EF.Access.Services.Resumes;

public class ResumeSkillsService : IResumeSkillsService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IResumeSkillsRepository resumeSkillsRepository;
    
    public ResumeSkillsService(IUnitOfWork unitOfWork, IResumeSkillsRepository resumeSkillsRepository)
    {
        this.unitOfWork = unitOfWork;
        this.resumeSkillsRepository = resumeSkillsRepository;
    }

    public async Task<OperationResult<ResumeSkillResponseDto>> CreateResumeSkillAsync(ResumeSkillCreateRequestDto request)
    {
        var resumeCompanyEntry = new ResumeSkill()
        {
            ResumeId = request.ResumeId,
            Name = request.Name,
            Level = request.Level
        };

        var createdEntity = await resumeSkillsRepository.CreateAsync(resumeCompanyEntry);
        await unitOfWork.SaveChangesAsync();

        return createdEntity.ToDto();
    }

    public async Task<OperationResult<ResumeSkillResponseDto>> UpdateResumeSkillAsync(ResumeSkillUpdateRequestDto request)
    {
        var resumeSkill = await resumeSkillsRepository.GetByIdAsync(request.Id);
        if (resumeSkill == null)
        {
            return OperationError.NotFound();
        }
        
        resumeSkill.Name = request.Name;
        resumeSkill.Level = request.Level;

        await resumeSkillsRepository.UpdateAsync(resumeSkill);
        await unitOfWork.SaveChangesAsync();
        
        return resumeSkill.ToDto();
    }

    public async Task<OperationResult<bool>> DeleteResumeSkillAsync(long companyId)
    {
        try
        {
            await resumeSkillsRepository.DeleteAsync(companyId);
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
        resumeSkillsRepository.Dispose();
    }
}