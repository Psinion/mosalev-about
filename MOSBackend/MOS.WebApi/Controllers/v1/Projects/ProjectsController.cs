using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MOS.Application.Data;
using MOS.Application.DTOs.Projects.Responses;
using MOS.Application.Modules.Projects.Commands;
using MOS.Application.Modules.Projects.Commands.Handlers;
using MOS.Application.Modules.Projects.Queries;
using MOS.Application.Modules.Projects.Queries.Handlers;
using MOS.Identity.Helpers;

namespace MOS.WebApi.Controllers.v1.Projects;

[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private readonly IHandlerFactory handlerFactory;
    
    public ProjectsController(IHandlerFactory handlerFactory)
    {
        this.handlerFactory = handlerFactory;
    }
    
    [HttpGet]
    [Route("list")]
    public async Task<ActionResult<ProjectCompactDto>> GetCompactProjectsList()
    {
        var getCompactsProjectsHandler = handlerFactory.GetHandler<IGetCompactProjectsHandler>();
        
        var response = await getCompactsProjectsHandler.Handle(new GetCompactProjectsQuery());
        
        return Ok(response.Value);
    }
    
    [HttpPost]
    [CustomAuthorize]
    public async Task<ActionResult<ProjectCompactDto>> CreateProject(CreateProjectCommand request)
    {
        var createProjectHandler = handlerFactory.GetHandler<ICreateProjectHandler>();
        
        var response = await createProjectHandler.Handle(request);
        
        return Ok(response.Value);
    }
}