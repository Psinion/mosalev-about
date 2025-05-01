using MOS.Application.Data.Repositories;
using MOS.Application.Data.Repositories.Projects;
using MOS.Application.Modules.Projects.Commands;
using MOS.Application.Modules.Projects.Commands.Handlers;
using MOS.Application.OperationResults;

namespace MOS.Data.EF.Access.Handlers.Articles;

public class DeleteArticleHandler : IDeleteArticleHandler
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IArticlesRepository articlesRepository;
    
    public DeleteArticleHandler(IUnitOfWork unitOfWork, IArticlesRepository articlesRepository)
    {
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
        
        await articlesRepository.DeleteAsync(project, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}