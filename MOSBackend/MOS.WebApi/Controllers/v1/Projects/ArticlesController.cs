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
    
    [HttpPost]
    [CustomAuthorize]
    public async Task<ActionResult<ArticleDto>> CreateArticle(CreateArticleCommand request)
    {
        var handler = handlerFactory.GetHandler<ICreateArticleHandler>();
        
        var response = await handler.Handle(request);
        
        return Ok(response.Value);
    }
}