using MOS.Application.DTOs.Projects.Responses;
using MOS.Domain.Entities.Projects;

namespace MOS.Application.Mappings.Projects;

public static class ArticlesMappings
{
    public static ArticleDto ToDto(this Article entity)
    {
        return new ArticleDto()
        {
            Id = entity.Id,
            ProjectId = entity.Project?.Id,
            Title = entity.Title,
            Description = entity.Description,
        };
    }
}