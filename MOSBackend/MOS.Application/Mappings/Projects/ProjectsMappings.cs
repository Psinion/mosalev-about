using MOS.Application.DTOs.Projects.Requests;
using MOS.Application.DTOs.Projects.Responses;
using MOS.Domain.Entities.Projects;

namespace MOS.Application.Mappings.Projects;


public static class ProjectsMappings
{
    public static ProjectResponseCompactDto ToCompactDto(this Project entity)
    {
        return new ProjectResponseCompactDto()
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
        };
    }
    
    public static List<ProjectResponseCompactDto> ToDtoList(this IList<Project> entities)
    {
        var dtoList = new List<ProjectResponseCompactDto>(entities.Count);
        foreach (var entity in entities)
        {
            dtoList.Add(entity.ToCompactDto());
        }
        return dtoList;
    }
    
    public static ProjectResponseDto ToDto(this Project entity)
    {
        return new ProjectResponseDto()
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
        };
    }
}