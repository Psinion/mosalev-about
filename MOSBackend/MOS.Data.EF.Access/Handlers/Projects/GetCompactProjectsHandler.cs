using Microsoft.EntityFrameworkCore;
using MOS.Application.Data.DbAccesses;
using MOS.Application.Data.Services.Users;
using MOS.Application.DTOs.Projects.Responses;
using MOS.Application.Modules.Projects.Extensions;
using MOS.Application.Modules.Projects.Queries;
using MOS.Application.Modules.Projects.Queries.Handlers;
using MOS.Application.OperationResults;
using MOS.Application.QueryObjects.Projects;

namespace MOS.Data.EF.Access.Handlers.Projects;

public class GetCompactProjectsHandler : IGetCompactProjectsHandler
{
    private readonly ICredentialsService credentialsService;
    private readonly IProjectsDbAccess projectsDbAccess;
    
    public GetCompactProjectsHandler(ICredentialsService credentialsService, IProjectsDbAccess projectsDbAccess)
    {
        this.credentialsService = credentialsService;
        this.projectsDbAccess = projectsDbAccess;
    }
    
    public async Task<OperationResult<List<ProjectCompactDto>>> Handle(GetCompactProjectsQuery request, CancellationToken cancellationToken = default)
    {
        var projects = await projectsDbAccess.GetProjects()
            .GetVisible(credentialsService)
            .OrderByDescending(x => x.UpdatedAt ?? x.CreatedAt)
            .MapProjectToCompactDto()
            .ToListAsync(cancellationToken);
        
        return projects;
    }
}