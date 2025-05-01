using MOS.Application.Data.Repositories;
using MOS.Application.Data.Repositories.Projects;
using MOS.Application.Data.Services.Users;
using MOS.Application.DTOs.Projects.Responses;
using MOS.Application.Mappings.Projects;
using MOS.Application.Modules.Projects.Commands;
using MOS.Application.Modules.Projects.Commands.Handlers;
using MOS.Application.OperationResults;
using MOS.Domain.Entities.Projects;

namespace MOS.Data.EF.Access.Handlers.Articles;

public class UpdateArticleHandler : IUpdateArticleHandler
{
    private readonly ICredentialsService credentialsService;
    private readonly IUnitOfWork unitOfWork;
    private readonly IProjectsRepository projectsRepository;
    private readonly IArticlesRepository articlesRepository;
    
    public UpdateArticleHandler(
        ICredentialsService credentialsService, 
        IUnitOfWork unitOfWork, 
        IProjectsRepository projectsRepository, 
        IArticlesRepository articlesRepository
    )
    {
        this.credentialsService = credentialsService;
        this.unitOfWork = unitOfWork;
        this.projectsRepository = projectsRepository;
        this.articlesRepository = articlesRepository;
    }
    
    public async Task<OperationResult<ArticleDto>> Handle(UpdateArticleCommand request, CancellationToken cancellationToken = default)
    {
        var article = await articlesRepository.GetByIdAsync(request.Id, cancellationToken);
        if (article == null)
        {
            return OperationError.NotFound("Article not found");
        }
        
        Project? project = null;
        
        if (request.ProjectId != null)
        {
            project = await projectsRepository.GetByIdAsync((int) request.ProjectId, cancellationToken);
            if (project == null)
            {
                return OperationError.NotFound("Project not found");
            }
        }
        
        article.ProjectId = project?.Id;
        article.Project = project;
        article.Title = request.Title;
        article.Description = request.Description;

        await articlesRepository.UpdateAsync(article, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return article.ToDto();
    }
}