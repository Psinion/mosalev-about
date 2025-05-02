using MOS.Application.Data.Repositories;
using MOS.Application.Data.Repositories.Projects;
using MOS.Application.Data.Services.Users;
using MOS.Application.Modules.Projects.Commands;
using MOS.Application.Modules.Projects.Commands.Handlers;
using MOS.Application.OperationResults;

namespace MOS.Data.EF.Access.Handlers.Articles;

public class DeleteArticleHandler : IDeleteArticleHandler
{
    private readonly ICredentialsService credentialsService;
    private readonly IUnitOfWork unitOfWork;
    private readonly IArticlesRepository articlesRepository;
    
    public DeleteArticleHandler(ICredentialsService credentialsService, IUnitOfWork unitOfWork, IArticlesRepository articlesRepository)
    {
        this.credentialsService = credentialsService;
        this.unitOfWork = unitOfWork;
        this.articlesRepository = articlesRepository;
    }

    public async Task<OperationResult<bool>> Handle(DeleteArticleCommand request, CancellationToken cancellationToken = default)
    {
        var project = await articlesRepository.GetByIdAsync(request.Id, cancellationToken);
        if (project == null)
        {
            return OperationError.NotFound();
        }
        
        project.UpdatedAt = DateTime.UtcNow;
        project.UpdatedBy = credentialsService.CurrentUser!.Id;
        
        await articlesRepository.DeleteAsync(project, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}