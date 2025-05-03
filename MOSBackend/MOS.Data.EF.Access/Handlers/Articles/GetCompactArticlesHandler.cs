using Microsoft.EntityFrameworkCore;
using MOS.Application.Data.DbAccesses;
using MOS.Application.Data.Services.Users;
using MOS.Application.DTOs.Projects.Responses;
using MOS.Application.Extensions;
using MOS.Application.Modules.Projects.Extensions;
using MOS.Application.Modules.Projects.Queries;
using MOS.Application.Modules.Projects.Queries.Handlers;
using MOS.Application.OperationResults;
using MOS.Application.QueryObjects.Projects;

namespace MOS.Data.EF.Access.Handlers.Articles;

public class GetCompactArticlesHandler : IGetCompactArticlesHandler
{
    private readonly ICredentialsService credentialsService;
    private readonly IArticlesDbAccess articlesDbAccess;

    public GetCompactArticlesHandler(ICredentialsService credentialsService, IArticlesDbAccess articlesDbAccess)
    {
        this.credentialsService = credentialsService;
        this.articlesDbAccess = articlesDbAccess;
    }
    
    public async Task<OperationResult<ArticlesCompactPaginationDto>> Handle(GetCompactArticlesQuery request, CancellationToken cancellationToken = default)
    {
        var query = articlesDbAccess
            .GetArticles()
            .GetVisible(credentialsService);

        if (request.OnlyFree)
        {
            query = query.Where(a => a.ProjectId == null);
        }
        
        var totalCount = await query.CountAsync(cancellationToken);
        var articles = await query
            .OrderByDescending(x => x.UpdatedAt ?? x.CreatedAt)
            .Page(request.Limit, request.Offset)
            .MapArticleByProjectToCompactDto()
            .ToListAsync(cancellationToken);
        
        return new ArticlesCompactPaginationDto()
        {
            Items = articles,
            TotalCount = totalCount,
        };
    }
}