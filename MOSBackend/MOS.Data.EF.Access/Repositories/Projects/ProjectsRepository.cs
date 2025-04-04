using MOS.Application.Data.Repositories.Projects;
using MOS.Data.EF.Access.Contexts;
using MOS.Domain.Entities.Projects;

namespace MOS.Data.EF.Access.Repositories.Projects;

public class ProjectsRepository : GenericRepository<Project>, IProjectsRepository
{
    public ProjectsRepository(MainDbContext dbLocalContext) : base(dbLocalContext)
    {
    }
}