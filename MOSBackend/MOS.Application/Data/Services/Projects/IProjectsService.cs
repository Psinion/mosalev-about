using MOS.Application.DTOs.Projects.Requests;
using MOS.Application.OperationResults;

namespace MOS.Application.Data.Services.Projects;

public interface IProjectsService : IDisposable
{
    Task<OperationResult<List<ProjectResponseCompactDto>>> GetCompactProjectsListAsync();
}