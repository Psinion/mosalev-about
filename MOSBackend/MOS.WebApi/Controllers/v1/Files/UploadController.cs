using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MOS.Application.DTOs.Files.Responses;
using MOS.Identity.Helpers;
using MOS.WebApi.Extensions;

namespace MOS.WebApi.Controllers.v1.Files;

[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class UploadController : ControllerBase
{
    private readonly IWebHostEnvironment webHostEnvironment;
    
    public UploadController(IWebHostEnvironment webHostEnvironment)
    {
        this.webHostEnvironment = webHostEnvironment;
    }
    
    [HttpGet("info")]
    [CustomAuthorize]
    public ActionResult<StorageInfoDto> GetStorageInfo()
    {
        var drive = new DriveInfo(Path.GetPathRoot(webHostEnvironment.GetUploadsPath()));

        var storage = new StorageInfoDto()
        {
            FreeSpace = drive.AvailableFreeSpace,
            TotalSize = drive.TotalSize,
        };
        
        return Ok(storage);
    }
}