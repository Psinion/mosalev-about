﻿using Asp.Versioning;
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
public class ResumeCompanyEntriesController : ControllerBase
{
    private readonly IResumeCompanyEntriesService resumeCompanyEntriesService;
    
    public ResumeCompanyEntriesController(IResumeCompanyEntriesService resumeCompanyEntriesService)
    {
        this.resumeCompanyEntriesService = resumeCompanyEntriesService;
    }
    
    [HttpPost]
    [CustomAuthorize]
    public async Task<ActionResult<ResumeCompanyEntryResponseDto>> CreateResumeCompanyEntry(ResumeCompanyEntryCreateRequestDto request)
    {
        var response = await resumeCompanyEntriesService.CreateResumeSkillAsync(request);
        return Ok(response.Value);
    }
    
    [HttpPut]
    [CustomAuthorize]
    public async Task<ActionResult<ResumeCompanyEntryResponseDto>> UpdateResumeCompanyEntry(ResumeCompanyEntryUpdateRequestDto request)
    {
        var response = await resumeCompanyEntriesService.UpdateResumeCompanyEntryAsync(request);

        if (response.Error.ErrorType == ErrorType.NotFound)
        {
            return NotFound(response.Error); 
        }
        
        return Ok(response.Value);
    }
    
    [HttpDelete]
    [CustomAuthorize]
    [Route("{companyId}")]
    public async Task<ActionResult<bool>> DeleteResumeCompanyEntry(int companyId)
    {
        var response = await resumeCompanyEntriesService.DeleteResumeCompanyEntryAsync(companyId);

        if (response.Error.ErrorType == ErrorType.NotFound)
        {
            return NotFound(response.Error); 
        }
        
        return Ok(response.Value);
    }
}