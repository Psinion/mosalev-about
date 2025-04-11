using MOS.Application.Data.Repositories.Projects;
using MOS.Application.Data.Services.Users;
using MOS.Data.EF.Access.Contexts;
using MOS.Domain.Entities.Projects;

namespace MOS.Data.EF.Access.Repositories.Projects;

public class ArticlesRepository : AuditableRepository<Article, long>, IArticlesRepository
{
    public ArticlesRepository(MainDbContext dbLocalContext, ICredentialsService credentialsService) 
        : base(dbLocalContext, credentialsService)
    {
    }
}