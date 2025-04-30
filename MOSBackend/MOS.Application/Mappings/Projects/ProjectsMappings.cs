using MOS.Application.DTOs.Projects.Responses;
using MOS.Domain.Entities.Projects;

namespace MOS.Application.Mappings.Projects;


public static class ProjectsMappings
{
    public static ProjectCompactDto ToCompactDto(this Project entity)
    {
        return new ProjectCompactDto()
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
        };
    }
    
    public static List<ProjectCompactDto> ToDtoList(this IList<Project> entities)
    {
        var dtoList = new List<ProjectCompactDto>(entities.Count);
        foreach (var entity in entities)
        {
            dtoList.Add(entity.ToCompactDto());
        }
        return dtoList;
    }
    
    public static ProjectDto ToDto(this Project entity)
    {
        return new ProjectDto()
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
        };
    }
}