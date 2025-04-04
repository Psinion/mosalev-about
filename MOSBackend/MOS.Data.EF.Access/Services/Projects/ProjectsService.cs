using MOS.Application.Data.Repositories.Projects;
using MOS.Application.Data.Services.Projects;

namespace MOS.Data.EF.Access.Services.Projects;

public class ProjectsService : IProjectsService
{
    private readonly IProjectsRepository projectsRepository;
    
    public ProjectsService(IProjectsRepository projectsRepository)
    {
        this.projectsRepository = projectsRepository;
    }

    public void Dispose()
    {
    }
}