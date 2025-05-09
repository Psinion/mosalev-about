using Microsoft.EntityFrameworkCore;
using MOS.Application.Data.DbAccesses;
using MOS.Application.Data.Services.Users;
using MOS.Application.DTOs.Projects.Responses;
using MOS.Application.Mappings.Projects;
using MOS.Application.Modules.Projects.Extensions;
using MOS.Application.Modules.Projects.Queries;
using MOS.Application.Modules.Projects.Queries.Handlers;
using MOS.Application.OperationResults;

namespace MOS.Data.EF.Access.Handlers.Projects;

public class GetProjectHandler : IGetProjectHandler
{
    private readonly ICredentialsService credentialsService;
    private readonly IProjectsDbAccess projectsDbAccess;
    
    public GetProjectHandler(ICredentialsService credentialsService, IProjectsDbAccess projectsDbAccess)
    {
        this.credentialsService = credentialsService;
        this.projectsDbAccess = projectsDbAccess;
    }
    
    public async Task<OperationResult<ProjectDto>> Handle(GetProjectQuery request, CancellationToken cancellationToken = default)
    {
        var project = await projectsDbAccess.GetProjects()
            .GetVisible(credentialsService)
            .FirstOrDefaultAsync(x => x.Id == request.ProjectId, cancellationToken);
        
        if (project == null)
        {
            return OperationError.NotFound("Not found");
        }

        return project.ToDto();
    }
}