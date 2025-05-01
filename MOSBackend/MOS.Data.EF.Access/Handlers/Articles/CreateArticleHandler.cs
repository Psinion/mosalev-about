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

public class CreateArticleHandler : ICreateArticleHandler
{
    private readonly ICredentialsService credentialsService;
    private readonly IUnitOfWork unitOfWork;
    private readonly IProjectsRepository projectsRepository;
    private readonly IArticlesRepository articlesRepository;
    
    public CreateArticleHandler(
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

    public async Task<OperationResult<ArticleDto>> Handle(CreateArticleCommand request, CancellationToken cancellationToken = default)
    {
        Project? project = null;
        
        if (request.ProjectId != null)
        {
            project = await projectsRepository.GetByIdAsync((int) request.ProjectId, cancellationToken);
            if (project == null)
            {
                return OperationError.NotFound("Project not found");
            }
        }
        
        var article = new Article()
        {
            Project = project,
            Title = request.Title,
            Description = request.Description,
            Visible = true,
            Locale = credentialsService.CurrentLocale,
        };

        var newProject = await articlesRepository.CreateAsync(article, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return newProject.ToDto();
    }
}