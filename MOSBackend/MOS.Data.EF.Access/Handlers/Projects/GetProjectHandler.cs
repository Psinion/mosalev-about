using Microsoft.EntityFrameworkCore;
using MOS.Application.DTOs.Projects.Responses;
using MOS.Application.Mappings.Projects;
using MOS.Application.Modules.Projects.Queries;
using MOS.Application.Modules.Projects.Queries.Handlers;
using MOS.Application.OperationResults;
using MOS.Data.EF.Access.Contexts;

namespace MOS.Data.EF.Access.Handlers.Projects;

public class GetProjectHandler : IGetProjectHandler
{
    private readonly MainDbContext mainDbContext;
    
    public GetProjectHandler(MainDbContext mainDbContext)
    {
        this.mainDbContext = mainDbContext;
    }
    
    public async Task<OperationResult<ProjectDto>> Handle(GetProjectQuery request, CancellationToken cancellationToken = default)
    {
        var project = await mainDbContext.Projects
            .FirstOrDefaultAsync(x => x.Id == request.ProjectId, cancellationToken);

        if (project == null)
        {
            return OperationError.NotFound("Not found");
        }

        return project.ToDto();
    }
}