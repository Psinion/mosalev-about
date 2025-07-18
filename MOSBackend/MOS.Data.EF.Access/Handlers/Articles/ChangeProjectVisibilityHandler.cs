using MOS.Application.Data.Repositories;
using MOS.Application.Data.Repositories.Projects;
using MOS.Application.Modules.Projects.Commands;
using MOS.Application.Modules.Projects.Commands.Handlers;
using MOS.Application.OperationResults;

namespace MOS.Data.EF.Access.Handlers.Articles;

public class ChangeArticleVisibilityHandler : IChangeArticleVisibilityHandler
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IArticlesRepository articlesRepository;

    public ChangeArticleVisibilityHandler(IUnitOfWork unitOfWork, IArticlesRepository articlesRepository)
    {
        this.unitOfWork = unitOfWork;
        this.articlesRepository = articlesRepository;
    }

    public async Task<OperationResult<bool>> Handle(ChangeArticleVisibilityCommand request, CancellationToken cancellationToken = default)
    {
        var article = await articlesRepository.GetByIdAsync(request.Id, cancellationToken);
        if (article == null)
        {
            return OperationError.NotFound();
        }

        article.Visible = request.Visible;

        await articlesRepository.UpdateAsync(article, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}