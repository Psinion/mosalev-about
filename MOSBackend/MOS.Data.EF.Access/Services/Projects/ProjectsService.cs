using Microsoft.EntityFrameworkCore;
using MOS.Application.Data.Repositories.Projects;
using MOS.Application.Data.Services.Projects;
using MOS.Application.DTOs.Projects.Requests;
using MOS.Application.DTOs.Projects.Responses;
using MOS.Application.Mappings.Projects;
using MOS.Application.OperationResults;
using MOS.Domain.Entities.Projects;

namespace MOS.Data.EF.Access.Services.Projects;

public class ProjectsService : IProjectsService
{
    private readonly IProjectsRepository projectsRepository;
    
    public ProjectsService(IProjectsRepository projectsRepository)
    {
        this.projectsRepository = projectsRepository;
    }

    public async Task<OperationResult<List<ProjectResponseCompactDto>>> GetCompactProjectsListAsync()
    {
        var resumes = await projectsRepository.GetAll()
            .Where(x => !x.IsDeleted)
            .OrderByDescending(x => x.CreatedAt)
            .ThenByDescending(x => x.UpdatedAt)
            .ToListAsync();

        return resumes.ToDtoList();
    }

    public async Task<OperationResult<ProjectResponseDto>> CreateProjectAsync(ProjectCreateRequestDto projectRequest)
    {
        var project = new Project()
        {
            Title = projectRequest.Title,
            Description = projectRequest.Description
        };

        var newProject = await projectsRepository.CreateAsync(project);

        return newProject.ToDto();
    }

    public void Dispose()
    {
        projectsRepository.Dispose();
    }
}