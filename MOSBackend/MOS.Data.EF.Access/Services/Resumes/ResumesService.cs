using Microsoft.EntityFrameworkCore;
using MOS.Application.Data.Repositories.Index;
using MOS.Application.Data.Repositories.Resumes;
using MOS.Application.Data.Services.IIndexService;
using MOS.Application.Data.Services.Resumes;
using MOS.Application.DTOs.Resumes.Requests;
using MOS.Application.DTOs.Resumes.Responses;
using MOS.Application.Mappings.Resumes;
using MOS.Application.OperationResults;
using MOS.Domain.Entities.Resumes;
using MOS.Domain.Enums;

namespace MOS.Data.EF.Access.Services.Resumes;

public class ResumesService : IResumesService
{
    private readonly IResumesRepository resumesRepository;
    
    public ResumesService(IResumesRepository resumesRepository)
    {
        this.resumesRepository = resumesRepository;
    }

    public async Task<OperationResult<ResumeResponseDto>> GetResumeAsync(long resumeId)
    {
        var resume = await resumesRepository.GetByIdAsync(resumeId);

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
            Email = resumeRequest.Email,
            Salary = resumeRequest.Salary,
            CurrencyType = resumeRequest.CurrencyType
        };

        var createdEntity = await resumesRepository.CreateAsync(resume);

        return createdEntity.ToDto();
    }

    public async Task<OperationResult<bool>> PinResumeAsync(long resumeId, Locale locale)
    {
        await resumesRepository.PinResumeAsync(resumeId, locale);

        return true;
    }
    
    public async Task<OperationResult<List<ResumeResponseCompactDto>>> GetCompactResumesListAsync()
    {
        var resumes = await resumesRepository.GetAll().Select(x => new Resume()
        {
            Id = x.Id,
            Title = x.Title,
            PinnedToLocale = x.PinnedToLocale
        }).ToListAsync();

        return resumes.ToCompactDtoList();
    }

    public void Dispose()
    {
        resumesRepository.Dispose();
    }
}