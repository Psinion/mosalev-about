using MOS.Application.Data;
using MOS.Application.DTOs.Projects.Responses;
using MOS.Application.OperationResults;

namespace MOS.Application.Modules.Projects.Queries;

public record GetProjectQuery : IQuery<OperationResult<ProjectDto>>
{
    public long ProjectId { get; init; }
    
    public GetProjectQuery(long projectId)
    {
        ProjectId = projectId;
    }
}