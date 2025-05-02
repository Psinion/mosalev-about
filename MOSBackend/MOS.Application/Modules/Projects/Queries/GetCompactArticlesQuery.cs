using MOS.Application.Data;
using MOS.Application.DTOs.Projects.Responses;
using MOS.Application.OperationResults;

namespace MOS.Application.Modules.Projects.Queries;

public record GetCompactArticlesQuery : IQuery<OperationResult<ArticlesCompactPaginationDto>>
{
    public bool OnlyFree { get; init; } = false;
    
    public int Limit { get; init; } = 0;
    public int Offset { get; init; } = 0;
}