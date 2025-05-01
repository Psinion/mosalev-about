using MOS.Application.Data;
using MOS.Application.DTOs.Projects.Responses;
using MOS.Application.OperationResults;

namespace MOS.Application.Modules.Projects.Queries;

public record GetCompactArticlesQuery : IQuery<OperationResult<ArticlesCompactPaginationDto>>
{
    /**
     * If -1, then all;
     * If null then all free articles;
     * If > 0 then all articles with that project.
     */
    public int? ProjectId { get; init; }
}