using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MOS.Application.Data.Services.Projects;
using MOS.Application.DTOs.Projects.Requests;

namespace MOS.WebApi.Controllers.v1.Projects;

[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private readonly IProjectsService projectsService;
    
    public ProjectsController(IProjectsService projectsService)
    {
        this.projectsService = projectsService;
    }
    
    [HttpGet]
    [Route("list")]
    public async Task<ActionResult<ProjectResponseCompactDto>> GetCompactProjectsList()
    {
        var response = await projectsService.GetCompactProjectsListAsync();
        return Ok(response.Value);
    }
}