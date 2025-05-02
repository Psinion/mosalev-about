using MOS.Application.DTOs.Projects.Responses;
using MOS.Application.Mappings.Projects;
using MOS.Domain.Entities.Projects;

namespace MOS.Application.QueryObjects.Projects;

public static class ProjectsQueryObjects
{
    public static IQueryable<ProjectCompactDto> MapProjectToCompactDto(this IQueryable<Project> books)    
    {
        return books.Select(entity => new ProjectCompactDto()
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            Visible = entity.Visible,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
        });
    }
}

public static class ArticlesQueryObjects
{
    public static IQueryable<ArticleCompactDto> MapArticleToCompactDto(this IQueryable<Article> articles)    
    {
        return articles.Select(entity => new ArticleCompactDto()
        {
            Id = entity.Id,
            ProjectId = entity.ProjectId,
            Title = entity.Title,
            Description = entity.Description.Substring(0, 360),
            Visible = entity.Visible,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
        });
    }
    
    public static IQueryable<ArticleDto> MapArticleToDto(this IQueryable<Article> articles)    
    {
        return articles.Select(entity => new ArticleDto()
        {
            Id = entity.Id,
            ProjectId = entity.ProjectId,
            Project = entity.Project != null ? entity.Project.ToCompactDto() : null,
            Title = entity.Title,
            Description = entity.Description,
            Visible = entity.Visible,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
        });
    }
}