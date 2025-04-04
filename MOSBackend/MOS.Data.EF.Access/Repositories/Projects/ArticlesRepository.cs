using MOS.Application.Data.Repositories.Projects;
using MOS.Data.EF.Access.Contexts;
using MOS.Domain.Entities.Projects;

namespace MOS.Data.EF.Access.Repositories.Projects;

public class ArticlesRepository : GenericRepository<Article>, IArticlesRepository
{
    public ArticlesRepository(MainDbContext dbLocalContext) : base(dbLocalContext)
    {
    }
}