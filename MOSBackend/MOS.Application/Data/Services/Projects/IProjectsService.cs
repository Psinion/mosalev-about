using MOS.Application.DTOs.Projects.Requests;
using MOS.Application.DTOs.Projects.Responses;
using MOS.Application.OperationResults;

namespace MOS.Application.Data.Services.Projects;

public interface IProjectsService : IDisposable
{
    Task<OperationResult<List<ProjectResponseCompactDto>>> GetCompactProjectsListAsync();
    
    Task<OperationResult<ProjectResponseDto>> CreateProjectAsync(ProjectCreateRequestDto projectRequest);
}