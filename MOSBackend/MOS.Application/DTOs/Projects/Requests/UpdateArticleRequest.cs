using System.ComponentModel.DataAnnotations;

namespace MOS.Application.DTOs.Projects.Requests;

public record UpdateArticleRequest
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