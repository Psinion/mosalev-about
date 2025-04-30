using MOS.Application.Data;
using MOS.Application.DTOs.Projects.Responses;
using MOS.Application.OperationResults;

namespace MOS.Application.Modules.Projects.Queries.Handlers;

public interface IGetCompactProjectsHandler : IQueryHandler<GetCompactProjectsQuery, OperationResult<List<ProjectCompactDto>>>
{
}