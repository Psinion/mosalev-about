using Microsoft.EntityFrameworkCore;
using MOS.Application.Data.DbAccesses;
using MOS.Application.Data.Services.Users;
using MOS.Application.DTOs.Projects.Responses;
using MOS.Application.Mappings.Projects;
using MOS.Application.Modules.Projects.Extensions;
using MOS.Application.Modules.Projects.Queries;
using MOS.Application.Modules.Projects.Queries.Handlers;
using MOS.Application.OperationResults;
using MOS.Application.QueryObjects.Projects;
using MOS.Data.EF.Access.Contexts;

namespace MOS.Data.EF.Access.Handlers.Articles;

public class GetArticleHandler : IGetArticleHandler
{
    private readonly ICredentialsService credentialsService;
    private readonly IArticlesDbAccess articleDbAccess;
    
    public GetArticleHandler(ICredentialsService credentialsService, IArticlesDbAccess articleDbAccess)
    {
        this.credentialsService = credentialsService;
        this.articleDbAccess = articleDbAccess;
    }
    
    public async Task<OperationResult<ArticleDto>> Handle(GetArticleQuery request, CancellationToken cancellationToken = default)
    {
        var article = await articleDbAccess.GetArticles()
            .GetVisible(credentialsService)
            .MapArticleToDto()
            .FirstOrDefaultAsync(x => x.Id == request.ArticleId, cancellationToken);
        
        if (article == null)
        {
            return OperationError.NotFound("Not found");
        }

        return article;
    }
}