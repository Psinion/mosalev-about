using MOS.Domain.Entities.Projects;

namespace MOS.Application.Data.Repositories.Projects;

public interface IProjectsRepository : IAuditableRepository<Project, int>
{
}