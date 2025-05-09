using MOS.Application.DTOs.Projects.Responses;
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