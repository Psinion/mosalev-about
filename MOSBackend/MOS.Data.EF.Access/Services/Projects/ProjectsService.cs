using Microsoft.EntityFrameworkCore;
using MOS.Application.Data.Repositories.Projects;
using MOS.Application.Data.Services.Projects;
using MOS.Application.DTOs.Projects.Requests;
using MOS.Application.Mappings.Projects;
using MOS.Application.OperationResults;

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
            .Where(x => x.DateDelete == null)
            .OrderByDescending(x => x.DateUpdate)
            .ToListAsync();

        return resumes.ToDtoList();
    }
    
    public void Dispose()
    {
        projectsRepository.Dispose();
    }
}