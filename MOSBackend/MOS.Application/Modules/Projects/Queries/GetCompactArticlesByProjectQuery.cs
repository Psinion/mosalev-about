using System.ComponentModel.DataAnnotations;
using MOS.Application.Data;
using MOS.Application.DTOs.Projects.Responses;
using MOS.Application.OperationResults;

namespace MOS.Application.Modules.Projects.Queries;

public record GetCompactArticlesByProjectQuery : IQuery<OperationResult<ArticlesCompactPaginationDto>>
{
    [Required]
    public int ProjectId { get; init; }
    
    public int Limit { get; init; } = 0;
    public int Offset { get; init; } = 0;
}