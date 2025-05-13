using System.ComponentModel.DataAnnotations;
using MOS.Application.Data;
using MOS.Application.DTOs.Projects.Responses;
using MOS.Application.OperationResults;

namespace MOS.Application.Modules.Projects.Commands;

public record UpdateArticleCommand : ICommand<OperationResult<ArticleDto>>
{
    public int Id { get; init; }
    
    /**
     * If project Id : null, then this article is free.
     * Otherwise, the article belongs to the project.
     */
    public int? ProjectId { get; init; }
    public string Title { get; init; } = ""; 
    public string Description { get; init; } = "";
}