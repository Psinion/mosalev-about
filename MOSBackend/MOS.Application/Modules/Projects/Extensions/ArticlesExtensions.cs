using MOS.Application.Data.Services.Users;
using MOS.Domain.Entities.Projects;

namespace MOS.Application.Modules.Projects.Extensions;

public static class ArticlesExtensions
{
    /**
     * Get all articles for creator and visible only for common users.
     */
    public static IQueryable<Article> GetVisible(this IQueryable<Article> query, ICredentialsService credentialsService)
    {
        if (credentialsService.CurrentUser == null)
        {
            query = query.Where(p => p.Visible == true);
        }
        
        query = query.Where(p => p.Locale == credentialsService.CurrentLocale);

        return query;
    }
}