using MOS.Application.Data.DbAccesses;
using MOS.Data.EF.Access.Contexts;
using MOS.Domain.Entities.Projects;

namespace MOS.Data.EF.Access.DbAccesses;

public class ProjectsDbAccess : IProjectsDbAccess
{
    private readonly MainDbContext context;

    public ProjectsDbAccess(MainDbContext context)
    {
        this.context = context;
    }
    
    public IQueryable<Project> GetProjects()
    {
        return context.Projects;
    }
}