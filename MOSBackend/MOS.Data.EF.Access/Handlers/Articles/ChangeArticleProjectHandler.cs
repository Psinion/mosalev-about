using MOS.Application.Data.Repositories;
using MOS.Application.Data.Repositories.Projects;
using MOS.Application.Modules.Projects.Commands;
using MOS.Application.Modules.Projects.Commands.Handlers;
using MOS.Application.OperationResults;

namespace MOS.Data.EF.Access.Handlers.Articles;

public class ChangeArticleProjectHandler : IChangeArticleProjectHandler
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IArticlesRepository articlesRepository;
    private readonly IProjectsRepository projectsRepository;

    public ChangeArticleProjectHandler(
        IUnitOfWork unitOfWork, 
        IArticlesRepository articlesRepository, 
        IProjectsRepository projectsRepository
        )
    {
        this.unitOfWork = unitOfWork;
        this.articlesRepository = articlesRepository;
        this.projectsRepository = projectsRepository;
    }
    
    public async Task<OperationResult<bool>> Handle(ChangeArticleProjectCommand request, CancellationToken cancellationToken = default)
    {
        var article = await articlesRepository.GetByIdAsync(request.Id, cancellationToken);
        if (article == null)
        {
            return OperationError.NotFound("Article not found");
        }

        if (request.ProjectId == null)
        {
            article.ProjectId = null;
            article.Project = null;
        }
        else
        {
            var project = await projectsRepository.GetByIdAsync((int)request.ProjectId, cancellationToken);
            if (project == null)
            {
                return OperationError.NotFound("Project not found");
            }
        
            article.ProjectId = request.ProjectId;
            article.Project = project;
        }

        await articlesRepository.UpdateAsync(article, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}