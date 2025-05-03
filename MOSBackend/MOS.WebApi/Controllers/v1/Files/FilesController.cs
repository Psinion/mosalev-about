using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MOS.Application.Data.Services.Files;
using MOS.Application.DTOs.Files.Responses;
using MOS.Application.OperationResults;
using MOS.Identity.Helpers;
using MOS.WebApi.Extensions;
using MOS.WebApi.Services.Files;

namespace MOS.WebApi.Controllers.v1.Files;

[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class FilesController : ControllerBase
{
    private readonly IFilesStorageService filesStorageService;
    
    public FilesController(IFilesStorageService filesStorageService)
    {
        this.filesStorageService = filesStorageService;
    }
    
    [HttpGet("info")]
    [CustomAuthorize]
    public ActionResult<StorageInfoDto> GetStorageInfo()
    {
        var drive = new DriveInfo(Path.GetPathRoot(filesStorageService.UploadsPath)!);

        var storage = new StorageInfoDto()
        {
            FreeSpace = drive.AvailableFreeSpace,
            TotalSize = drive.TotalSize,
        };
        
        return Ok(storage);
    }
    
    [HttpPost]
    [CustomAuthorize]
    [RequestSizeLimit(FilesStorageService.MaxFileSize)]
    public async Task<ActionResult<StorageInfoDto>> CreateFile(IFormFile file)
    {
        if (!filesStorageService.ValidateFile(file))
        {
            return BadRequest("Invalid file type or size");
        }

        try
        {
            var fileToUpload = await filesStorageService.SaveFileAsync(file);
        }
        catch (Exception ex)
        {
            return BadRequest("Failed to save file");
        }
        
        return Ok();
    }
}