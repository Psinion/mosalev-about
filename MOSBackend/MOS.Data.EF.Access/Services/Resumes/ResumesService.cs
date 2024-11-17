using MOS.Application.Data.Repositories.Index;
using MOS.Application.Data.Repositories.Resumes;
using MOS.Application.Data.Services.IIndexService;
using MOS.Application.Data.Services.Resumes;
using MOS.Application.DTOs.Resumes.Requests;
using MOS.Application.DTOs.Resumes.Responses;
using MOS.Application.DTOs.Users.Responses;
using MOS.Application.OperationResults;
using MOS.Domain.Entities.Resumes;

namespace MOS.Data.EF.Access.Services.Resumes;

public class ResumesService : IResumesService
{
    private readonly IResumesRepository resumesRepository;
    
    public ResumesService(IResumesRepository resumesRepository)
    {
        this.resumesRepository = resumesRepository;
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

        return new ResumeResponseDto(createdEntity);
    }

    public void Dispose()
    {
        resumesRepository.Dispose();
    }
}