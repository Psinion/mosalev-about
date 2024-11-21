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
    public async Task<ActionResult<ResumeResponseCompactDto>> GetResume(long resumeId)
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
    [Route("{resumeId}")]
    public async Task<ActionResult<ResumeResponseDto>> PinResume(long resumeId, ResumePinRequestDto request)
    {
        var response = await resumesService.PinResumeAsync(resumeId, request.Locale);
        return Ok(response.Value);
    }
}