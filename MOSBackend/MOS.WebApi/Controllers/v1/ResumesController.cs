using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MOS.Application.Data.Services.Resumes;
using MOS.Application.DTOs.Resumes.Requests;
using MOS.Application.DTOs.Resumes.Responses;

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
    
    [HttpPost]
    [Route("resume")]
    public async Task<ActionResult<ResumeResponseDto>> CreateResume(ResumeCreateRequestDto request)
    {
        var response = await resumesService.CreateResumeAsync(request);
        return Ok(response.Value);
    }
    
    [HttpGet]
    [Route("resume/list")]
    public async Task<ActionResult<ResumeResponseCompactDto>> GetCompactResumeList()
    {
        var response = await resumesService.GetCompactResumesListAsync();
        return Ok(response.Value);
    }
}