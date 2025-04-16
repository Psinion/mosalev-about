using MOS.Application.Data.Repositories;
using MOS.Application.Data.Repositories.Projects;
using MOS.Application.Data.Services.Projects;

namespace MOS.Data.EF.Access.Services.Projects;

public class ArticlesService : IArticlesService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IArticlesRepository articlesRepository;
    
    public ArticlesService(IUnitOfWork unitOfWork, IArticlesRepository articlesRepository)
    {
        this.articlesRepository = articlesRepository;
    }

    public void Dispose()
    {
    }
}