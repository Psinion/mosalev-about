using Microsoft.EntityFrameworkCore;
using MOS.Application.Data.Repositories.Projects;
using MOS.Application.DTOs.Projects.Responses;
using MOS.Application.Modules.Projects.Queries;
using MOS.Application.Modules.Projects.Queries.Handlers;
using MOS.Application.OperationResults;
using MOS.Application.QueryObjects.Projects;

namespace MOS.Data.EF.Access.Handlers.Projects;

public class GetCompactProjectsHandler : IGetCompactProjectsHandler
{
    private readonly IProjectsRepository projectsRepository;
    
    public GetCompactProjectsHandler(IProjectsRepository projectsRepository)
    {
        this.projectsRepository = projectsRepository;
    }
    
    public async Task<OperationResult<List<ProjectCompactDto>>> Handle(GetCompactProjectsQuery request, CancellationToken cancellationToken = default)
    {
        var resumes = await projectsRepository.GetAll()
            .Where(x => !x.IsDeleted)
            .OrderByDescending(x => x.CreatedAt)
            .ThenByDescending(x => x.UpdatedAt)
            .MapProjectToCompactDto()
            .ToListAsync(cancellationToken);

        return resumes;
    }
}