using Microsoft.EntityFrameworkCore;
using MOS.Application.Data.Repositories.Resumes;
using MOS.Application.Data.Services.Resumes;
using MOS.Application.Data.Services.Users;
using MOS.Application.DTOs.Resumes.Requests;
using MOS.Application.DTOs.Resumes.Responses;
using MOS.Application.Mappings.Resumes;
using MOS.Application.OperationResults;
using MOS.Domain.Entities.Resumes;
using MOS.Domain.Entities.Users;

namespace MOS.Data.EF.Access.Services.Resumes;

public class ResumesService : IResumesService
{
    private readonly ICredentialsService credentialsService;
    private readonly IResumesRepository resumesRepository;
    
    public ResumesService(ICredentialsService credentialsService, IResumesRepository resumesRepository)
    {
        this.credentialsService = credentialsService;
        this.resumesRepository = resumesRepository;
    }

    public async Task<OperationResult<ResumeResponseDto>> GetResumeAsync(long resumeId)
    {
        var resume = await resumesRepository.GetByIdWithRelationsAsync(resumeId);

        if (resume == null)
        {
            return OperationError.NotFound("Not found");
        }

        return resume.ToDto();
    }

    public async Task<OperationResult<ResumeResponseDto>> CreateResumeAsync(ResumeCreateRequestDto resumeRequest)
    {
        var resume = new Resume()
        {
            Title = resumeRequest.Title,
            About = resumeRequest.About,
        };

        var createdEntity = await resumesRepository.CreateAsync(resume);

        return createdEntity.ToDto();
    }

    public async Task<OperationResult<ResumeResponseDto>> UpdateResumeAsync(ResumeUpdateRequestDto resumeRequest)
    {
        var resume = await resumesRepository.GetByIdAsync(resumeRequest.Id);
        if (resume == null)
        {
            return OperationError.NotFound();
        }

        resume.Title = resumeRequest.Title;
        resume.About = resumeRequest.About;

        await resumesRepository.UpdateAsync(resume);
        
        return resume.ToDto();
    }
    
    public async Task<OperationResult<bool>> HasPinnedResumeAsync()
    {
        return await resumesRepository.GetPinnedResumeAsync(credentialsService.CurrentLocale) != null;
    }

    public async Task<OperationResult<ResumeResponseDto>> GetPinnedResumeAsync()
    {
        var resume = await resumesRepository.GetPinnedResumeAsync(credentialsService.CurrentLocale);

        if (resume == null)
        {
            return OperationError.NotFound(); 
        }

        return resume.ToDto();
    }

    public async Task<OperationResult<bool>> PinResumeAsync(long resumeId, bool pinning)
    {
        if (!resumesRepository.ExistsAsync(resumeId).Result)
        {
            return OperationError.NotFound();
        }

        await resumesRepository.UnpinAllByLocaleAsync(credentialsService.CurrentLocale);
        await resumesRepository.PinResumeAsync(resumeId, pinning ? credentialsService.CurrentLocale : null);

        return true;
    }
    
    public async Task<OperationResult<List<ResumeResponseCompactDto>>> GetCompactResumesListAsync()
    {
        var resumes = await resumesRepository.GetAll()
            .Where(x => x.DateDelete == null)
            .OrderByDescending(x => x.DateUpdate)
            .Select(x => new Resume()
        {
            Id = x.Id,
            Title = x.Title,
            PinnedToLocale = x.PinnedToLocale,
            DateCreate = x.DateCreate,
            UserCreate = x.UserCreate != null ? new User()
            {
                Id = x.UserCreate.Id,
                FirstName = x.UserCreate.FirstName,
                LastName = x.UserCreate.LastName,
                Patronymic = x.UserCreate.Patronymic,
                UserName = x.UserCreate.UserName,
            } : null,
            DateUpdate = x.DateUpdate,
            UserUpdate = x.UserUpdate != null ? new User()
            {
                Id = x.UserUpdate.Id,
                FirstName = x.UserUpdate.FirstName,
                LastName = x.UserUpdate.LastName,
                Patronymic = x.UserUpdate.Patronymic,
                UserName = x.UserUpdate.UserName,
            } : null,
        }).ToListAsync();

        return resumes.ToCompactDtoList();
    }

    public void Dispose()
    {
        credentialsService.Dispose();
        resumesRepository.Dispose();
    }
}