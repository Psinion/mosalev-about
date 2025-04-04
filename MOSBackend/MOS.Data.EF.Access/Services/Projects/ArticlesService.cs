using MOS.Application.Data.Repositories.Projects;
using MOS.Application.Data.Services.Projects;

namespace MOS.Data.EF.Access.Services.Projects;

public class ArticlesService : IArticlesService
{
    private readonly IArticlesRepository articlesRepository;
    
    public ArticlesService(IArticlesRepository articlesRepository)
    {
        this.articlesRepository = articlesRepository;
    }

    public void Dispose()
    {
    }
}