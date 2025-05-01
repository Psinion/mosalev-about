using MOS.Domain.Entities.Projects;

namespace MOS.Application.Data.Repositories.Projects;

public interface IArticlesRepository : IAuditableRepository<Article, int>
{
}