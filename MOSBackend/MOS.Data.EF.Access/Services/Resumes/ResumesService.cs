using MOS.Application.Data.Repositories.Index;
using MOS.Application.Data.Repositories.Resumes;
using MOS.Application.Data.Services.IIndexService;
using MOS.Application.Data.Services.Resumes;
using MOS.Application.DTOs.Users.Responses;
using MOS.Application.OperationResults;

namespace MOS.Data.EF.Access.Services.Resumes;

public class ResumesService : IResumesService
{
    private readonly IResumesRepository resumesRepository;
    
    public ResumesService(IResumesRepository resumesRepository)
    {
        this.resumesRepository = resumesRepository;
    }

    public void Dispose()
    {
        resumesRepository.Dispose();
    }
}