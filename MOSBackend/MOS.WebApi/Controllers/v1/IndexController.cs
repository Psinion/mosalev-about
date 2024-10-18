using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MOS.Application.Data.Services.IIndexService;
using MOS.Application.DTOs.Users.Responses;
using MOS.Application.OperationResults.Enums;

namespace MOS.WebApi.Controllers.v1;

[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class IndexController : ControllerBase
{
    private readonly IIndexService indexService;
    
    public IndexController(IIndexService indexService)
    {
        this.indexService = indexService;
    }
    
    [HttpGet]
    [Route("actual_index_content")]
    public async Task<ActionResult<AuthenticateResponseDto>> GetActualIndexContent()
    {
        var response = await indexService.GetActualIndexContentAsync();

        if (response.Error.ErrorType == ErrorType.NotFound)
        {
            return NotFound(response.Error);
        }

        return Ok(response.Value);
    }
}