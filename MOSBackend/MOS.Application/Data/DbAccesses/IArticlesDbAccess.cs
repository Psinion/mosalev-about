using MOS.Domain.Entities.Projects;

namespace MOS.Application.Data.DbAccesses;

public interface IArticlesDbAccess
{
    IQueryable<Article> GetArticles();
}