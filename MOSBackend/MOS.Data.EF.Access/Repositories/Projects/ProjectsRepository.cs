using MOS.Application.Data.Repositories.Projects;
using MOS.Application.Data.Services.Users;
using MOS.Data.EF.Access.Contexts;
using MOS.Domain.Entities.Projects;

namespace MOS.Data.EF.Access.Repositories.Projects;

public class ProjectsRepository : AuditableRepository<Project, int>, IProjectsRepository
{
    public ProjectsRepository(MainDbContext dbLocalContext, ICredentialsService credentialsService) 
        : base(dbLocalContext, credentialsService)
    {
    }
}