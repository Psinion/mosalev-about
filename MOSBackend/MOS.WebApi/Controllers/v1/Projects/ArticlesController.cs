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
public class ArticlesController : ControllerBase
{
    private readonly IHandlerFactory handlerFactory;
    
    public ArticlesController(IHandlerFactory handlerFactory)
    {
        this.handlerFactory = handlerFactory;
    }
    
    [HttpGet("")]
    public async Task<ActionResult<ArticlesCompactPaginationDto>> GetArticles([FromQuery] GetCompactArticlesQuery request)
    {
        var handler = handlerFactory.GetHandler<IGetCompactArticlesHandler>();
        
        var response = await handler.Handle(request);
        
        return Ok(response.Value);
    }
    
    [HttpGet("by-project")]
    public async Task<ActionResult<ArticlesCompactPaginationDto>> GetArticlesByProject([FromQuery] GetCompactArticlesByProjectQuery request)
    {
        var handler = handlerFactory.GetHandler<IGetCompactArticlesByProjectHandler>();
        
        var response = await handler.Handle(request);
        
        return Ok(response.Value);
    }
    
    [HttpGet("{articleId}")]
    public async Task<ActionResult<ArticleDto>> GetArticle(int articleId)
    {
        var handler = handlerFactory.GetHandler<IGetArticleHandler>();
        
        var response = await handler.Handle(new GetArticleQuery(articleId));
        
        if (response.Error.ErrorType == ErrorType.NotFound)
        {
            return NotFound(response.Error);
        }
        
        return Ok(response.Value);
    }
    
    [HttpPost]
    [CustomAuthorize]
    public async Task<ActionResult<ArticleDto>> CreateArticle(CreateArticleCommand request)
    {
        var handler = handlerFactory.GetHandler<ICreateArticleHandler>();
        
        var response = await handler.Handle(request);
        
        return Ok(response.Value);
    }
    
    [HttpPut("{articleId}")]
    [CustomAuthorize]
    public async Task<ActionResult<ArticleDto>> UpdateArticle(int articleId, UpdateArticleRequest request)
    {
        var command = new UpdateArticleCommand()
        {
            Id = articleId,
            ProjectId = request.ProjectId,
            Title = request.Title,
            Description = request.Description,
        };
        
        var handler = handlerFactory.GetHandler<IUpdateArticleHandler>();
        
        var response = await handler.Handle(command);
        
        return Ok(response.Value);
    }
    
    [HttpDelete("{articleId}")]
    [CustomAuthorize]
    public async Task<ActionResult<bool>> DeleteArticle(int articleId)
    {
        var handler = handlerFactory.GetHandler<IDeleteArticleHandler>();
        
        var response = await handler.Handle(new DeleteArticleCommand(articleId));
        
        return Ok(response.Value);
    }
    
    [HttpPatch("{articleId}/visibility")]
    [CustomAuthorize]
    public async Task<ActionResult<bool>> ChangeArticleVisibility(int articleId, ChangeArticleVisibilityRequest request)
    {
        var command = new ChangeArticleVisibilityCommand()
        {
            Id = articleId,
            Visible = request.Visible,
        };
        
        var handler = handlerFactory.GetHandler<IChangeArticleVisibilityHandler>();
        
        var response = await handler.Handle(command);
        
        if (response.Error.ErrorType == ErrorType.NotFound)
        {
            return NotFound(response.Error);
        }
        
        return Ok(response.Value);
    }
    
    [HttpPatch("{articleId}/project")]
    [CustomAuthorize]
    public async Task<ActionResult<bool>> ChangeArticleProject(int articleId, ChangeArticleProjectRequest request)
    {
        var command = new ChangeArticleProjectCommand()
        {
            Id = articleId,
            ProjectId = request.ProjectId,
        };
        
        var handler = handlerFactory.GetHandler<IChangeArticleProjectHandler>();
        
        var response = await handler.Handle(command);
        
        if (response.Error.ErrorType == ErrorType.NotFound)
        {
            return NotFound(response.Error);
        }
        
        return Ok(response.Value);
    }
}