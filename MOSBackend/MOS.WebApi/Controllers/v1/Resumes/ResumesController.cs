using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MOS.Application.Data.Services.Resumes;
using MOS.Application.DTOs.Resumes.Requests;
using MOS.Application.DTOs.Resumes.Responses;
using MOS.Application.OperationResults.Enums;
using MOS.Identity.Helpers;

namespace MOS.WebApi.Controllers.v1.Resumes;

[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class ResumesController : ControllerBase
{
    private readonly IResumesService resumesService;
    
    public ResumesController(IResumesService resumesService)
    {
        this.resumesService = resumesService;
    }
    
    [HttpGet]
    [CustomAuthorize]
    [Route("list")]
    public async Task<ActionResult<ResumeResponseCompactDto>> GetCompactResumeList()
    {
        var response = await resumesService.GetCompactResumesListAsync();
        return Ok(response.Value);
    }
    
    [HttpGet]
    [CustomAuthorize]
    [Route("{resumeId}")]
    public async Task<ActionResult<ResumeResponseDto>> GetResume(int resumeId)
    {
        var response = await resumesService.GetResumeAsync(resumeId);

        if (response.Error.ErrorType == ErrorType.NotFound)
        {
            return NotFound(response.Error);
        }
        
        return Ok(response.Value);
    }
    
    [HttpPost]
    [CustomAuthorize]
    public async Task<ActionResult<ResumeResponseDto>> CreateResume(ResumeCreateRequestDto request)
    {
        var response = await resumesService.CreateResumeAsync(request);
        return Ok(response.Value);
    }
    
    [HttpPut]
    [CustomAuthorize]
    public async Task<ActionResult<ResumeResponseDto>> UpdateResume(ResumeUpdateRequestDto request)
    {
        var response = await resumesService.UpdateResumeAsync(request);

        if (response.Error.ErrorType == ErrorType.NotFound)
        {
            return NotFound(response.Error); 
        }
        
        return Ok(response.Value);
    }
    
    [HttpHead]
    [Route("pin")]
    public async Task<ActionResult> HasPinnedResume()
    {
        var response = await resumesService.HasPinnedResumeAsync();

        if (!response.Value)
        {
            return NotFound();
        }
        
        return Ok();
    }
    
    [HttpGet]
    [Route("pin")]
    public async Task<ActionResult<ResumeResponseDto>> GetPinnedResume()
    {
        var response = await resumesService.GetPinnedResumeAsync();

        if (response.Error.ErrorType == ErrorType.NotFound)
        {
            return NotFound();
        }
        
        return Ok(response.Value);
    }
    
    [HttpPut]
    [CustomAuthorize]
    [Route("{resumeId}/pin")]
    public async Task<ActionResult> PinResume(int resumeId)
    {
        var response = await resumesService.PinResumeAsync(resumeId, true);

        if (response.Error.ErrorType == ErrorType.NotFound)
        {
            return NotFound();
        }
        
        return Ok();
    }
    
    [HttpPut]
    [CustomAuthorize]
    [Route("{resumeId}/unpin")]
    public async Task<ActionResult> UnpinResume(int resumeId)
    {
        var response = await resumesService.PinResumeAsync(resumeId, false);
        
        if (response.Error.ErrorType == ErrorType.NotFound)
        {
            return NotFound();
        }
        
        return Ok();
    }
}