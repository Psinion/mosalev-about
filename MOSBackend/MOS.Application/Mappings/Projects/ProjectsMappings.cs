using MOS.Application.DTOs.Projects.Requests;
using MOS.Domain.Entities.Projects;

namespace MOS.Application.Mappings.Projects;


public static class ProjectsMappings
{
    public static ProjectResponseCompactDto ToDto(this Project entity)
    {
        return new ProjectResponseCompactDto()
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
        };
    }
    
    public static List<ProjectResponseCompactDto> ToDtoList(this IList<Project> entities)
    {
        var dtoList = new List<ProjectResponseCompactDto>(entities.Count);
        foreach (var entity in entities)
        {
            dtoList.Add(entity.ToDto());
        }
        return dtoList;
    }
}