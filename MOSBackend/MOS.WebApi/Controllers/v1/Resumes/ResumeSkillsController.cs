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
public class ResumeSkillsController : ControllerBase
{
    private readonly IResumeSkillsService resumeSkillsService;
    
    public ResumeSkillsController(IResumeSkillsService resumeSkillsService)
    {
        this.resumeSkillsService = resumeSkillsService;
    }
    
    [HttpPost]
    [CustomAuthorize]
    public async Task<ActionResult<ResumeCompanyEntryResponseDto>> CreateResumeSkill(ResumeSkillCreateRequestDto request)
    {
        var response = await resumeSkillsService.CreateResumeSkillAsync(request);
        return Ok(response.Value);
    }
    
    [HttpPut]
    [CustomAuthorize]
    public async Task<ActionResult<ResumeCompanyEntryResponseDto>> UpdateResumeCompanyEntry(ResumeSkillUpdateRequestDto request)
    {
        var response = await resumeSkillsService.UpdateResumeSkillAsync(request);

        if (response.Error.ErrorType == ErrorType.NotFound)
        {
            return NotFound(response.Error); 
        }
        
        return Ok(response.Value);
    }
    
    [HttpDelete]
    [CustomAuthorize]
    [Route("{skillId}")]
    public async Task<ActionResult<bool>> DeleteResumeCompanyEntry(long skillId)
    {
        var response = await resumeSkillsService.DeleteResumeSkillAsync(skillId);

        if (response.Error.ErrorType == ErrorType.NotFound)
        {
            return NotFound(response.Error); 
        }
        
        return Ok(response.Value);
    }
}