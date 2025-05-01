using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MOS.Application.Data;
using MOS.Application.DTOs.Projects.Requests;
using MOS.Application.DTOs.Projects.Responses;
using MOS.Application.Modules.Projects.Commands;
using MOS.Application.Modules.Projects.Commands.Handlers;
using MOS.Application.Modules.Projects.Queries;
using MOS.Application.Modules.Projects.Queries.Handlers;
using MOS.Application.OperationResults.Enums;
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
        var handler = handlerFactory.GetHandler<IGetCompactProjectsHandler>();
        
        var response = await handler.Handle(new GetCompactProjectsQuery());
        
        return Ok(response.Value);
    }
    
    [HttpGet]
    [Route("{projectId}")]
    public async Task<ActionResult<ProjectCompactDto>> GetProject(int projectId)
    {
        var handler = handlerFactory.GetHandler<IGetProjectHandler>();
        
        var response = await handler.Handle(new GetProjectQuery(projectId));
        
        if (response.Error.ErrorType == ErrorType.NotFound)
        {
            return NotFound(response.Error);
        }
        
        return Ok(response.Value);
    }
    
    [HttpPost]
    [CustomAuthorize]
    public async Task<ActionResult<ProjectCompactDto>> CreateProject(CreateProjectCommand request)
    {
        var handler = handlerFactory.GetHandler<ICreateProjectHandler>();
        
        var response = await handler.Handle(request);
        
        return Ok(response.Value);
    }
    
    [HttpPut("{projectId}")]
    [CustomAuthorize]
    public async Task<ActionResult<ProjectCompactDto>> UpdateProject(int projectId, UpdateProjectRequest request)
    {
        var command = new UpdateProjectCommand()
        {
            Id = projectId,
            Title = request.Title,
            Description = request.Description,
        };
        
        var handler = handlerFactory.GetHandler<IUpdateProjectHandler>();
        
        var response = await handler.Handle(command);
        
        if (response.Error.ErrorType == ErrorType.NotFound)
        {
            return NotFound(response.Error);
        }
        
        return Ok(response.Value);
    }
    
    [HttpDelete("{projectId}")]
    [CustomAuthorize]
    public async Task<ActionResult<bool>> DeleteProject(int projectId)
    {
        var handler = handlerFactory.GetHandler<IDeleteProjectHandler>();
        
        var response = await handler.Handle(new DeleteProjectCommand(projectId));
        
        if (response.Error.ErrorType == ErrorType.NotFound)
        {
            return NotFound(response.Error);
        }
        
        return Ok(response.Value);
    }    
    
    [HttpPatch("{projectId}/visibility")]
    [CustomAuthorize]
    public async Task<ActionResult<bool>> ChangeProjectVisibility(int projectId, ChangeProjectVisibilityRequest request)
    {
        var command = new ChangeProjectVisibilityCommand()
        {
            Id = projectId,
            Visible = request.Visible,
        };
        
        var handler = handlerFactory.GetHandler<IChangeProjectVisibilityHandler>();
        
        var response = await handler.Handle(command);
        
        if (response.Error.ErrorType == ErrorType.NotFound)
        {
            return NotFound(response.Error);
        }
        
        return Ok(response.Value);
    }
}