using MOS.Application.Data.Services.Users;
using MOS.Domain.Entities.Projects;

namespace MOS.Application.Modules.Projects.Extensions;

public static class ProjectsExtensions
{
    /**
     * Get all projects for creator and visible only for common users.
     */
    public static IQueryable<Project> GetVisible(this IQueryable<Project> query, ICredentialsService credentialsService)
    {
        if (credentialsService.CurrentUser == null)
        {
            query = query.Where(p => p.Visible == true);
        }

        return query;
    }
}