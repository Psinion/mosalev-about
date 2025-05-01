using MOS.Domain.Entities.Projects;

namespace MOS.Application.Data.DbAccesses;

public interface IProjectsDbAccess
{
    IQueryable<Project> GetProjects();
}