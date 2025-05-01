using MOS.Application.Data;
using MOS.Application.DTOs.Projects.Responses;
using MOS.Application.OperationResults;

namespace MOS.Application.Modules.Projects.Queries;

public record GetArticleQuery : IQuery<OperationResult<ArticleDto>>
{
    public int ArticleId { get; init; }
    
    public GetArticleQuery(int articleId)
    {
        ArticleId = articleId;
    }
}