using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MOS.Application.Data.Services.Resumes;
using MOS.Application.DTOs.Resumes.Requests;
using MOS.Application.DTOs.Resumes.Responses;
using MOS.Application.OperationResults.Enums;
using MOS.Identity.Helpers;

namespace MOS.WebApi.Controllers.v1;

[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class ResumePostsController : ControllerBase
{
    private readonly IResumePostsService resumePostsService;
    
    public ResumePostsController(IResumePostsService resumePostsService)
    {
        this.resumePostsService = resumePostsService;
    }
    
    [HttpPost]
    [CustomAuthorize]
    public async Task<ActionResult<ResumePostResponseDto>> CreateResumePost(ResumePostCreateRequestDto request)
    {
        var response = await resumePostsService.CreateResumePostAsync(request);
        return Ok(response.Value);
    }
    
    [HttpPut]
    [CustomAuthorize]
    public async Task<ActionResult<ResumePostResponseDto>> UpdateResumePost(ResumePostUpdateRequestDto request)
    {
        var response = await resumePostsService.UpdateResumePostAsync(request);

        if (response.Error.ErrorType == ErrorType.NotFound)
        {
            return NotFound(response.Error); 
        }
        
        return Ok(response.Value);
    }
    
    [HttpDelete]
    [CustomAuthorize]
    [Route("{postId}")]
    public async Task<ActionResult<bool>> DeleteResumePost(long postId)
    {
        var response = await resumePostsService.DeleteResumePostAsync(postId);

        if (response.Error.ErrorType == ErrorType.NotFound)
        {
            return NotFound(response.Error); 
        }
        
        return Ok(response.Value);
    }
}