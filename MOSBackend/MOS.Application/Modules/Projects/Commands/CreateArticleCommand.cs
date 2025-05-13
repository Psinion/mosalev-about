using System.ComponentModel.DataAnnotations;
using MOS.Application.Data;
using MOS.Application.DTOs.Projects.Responses;
using MOS.Application.OperationResults;

namespace MOS.Application.Modules.Projects.Commands;

public record CreateArticleCommand : ICommand<OperationResult<ArticleDto>>
{
    /**
     * If project Id : null, then this article is free.
     * Otherwise, the article belongs to the project.
     */
    public int? ProjectId { get; init; }
    
    [Required]
    [StringLength(64)] 
    public string Title { get; init; } = "";

    [Required] public string Description { get; init; } = "";
}