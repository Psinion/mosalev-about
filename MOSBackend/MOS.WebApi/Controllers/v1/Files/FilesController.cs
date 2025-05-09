﻿using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using MOS.Application.Data;
using MOS.Application.Data.Services.Files;
using MOS.Application.DTOs.Files.Responses;
using MOS.Application.Modules.Files.Commands;
using MOS.Application.Modules.Files.Commands.Handlers;
using MOS.Application.Modules.Files.Queries;
using MOS.Application.Modules.Files.Queries.Handlers;
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
    private readonly IHandlerFactory handlerFactory;
    private readonly IFilesStorageService filesStorageService;
    
    public FilesController(IHandlerFactory handlerFactory, IFilesStorageService filesStorageService)
    {
        this.handlerFactory = handlerFactory;
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
    //[CustomAuthorize]
    [RequestSizeLimit(FilesStorageService.MaxFileSize)]
    public async Task<ActionResult<UploadedFileDto>> CreateFile(IFormFile file)
    {
        if (!filesStorageService.ValidateFile(file))
        {
            return BadRequest("Invalid file type or size");
        }

        try
        {
            var fileToUpload = await filesStorageService.SaveFileAsync(file);
            
            var handler = handlerFactory.GetHandler<ICreateFileHandler>();
        
            var savedFile = await handler.Handle(new CreateFileCommand(fileToUpload));
            
            return Ok(savedFile);
        }
        catch (Exception ex)
        {
            return BadRequest("Failed to save file");
        }
    }
    
    [HttpGet("{fileId}")]
    public async Task<ActionResult<StorageInfoDto>> GetFile(int fileId)
    {
        var handler = handlerFactory.GetHandler<IGetFileHandler>();
        
        var file = await handler.Handle(new GetFileQuery(fileId));
        
        return Ok(file);
    }
    
    [HttpGet("image/{**imagePath}")]
    [ResponseCache(Duration = 60 * 60 * 24 * 30)]
    public ActionResult<StorageInfoDto> GetImage(string imagePath)
    {  
        var allowedExtensions = new[] { ".jpg", ".png", ".gif" };
        var extension = Path.GetExtension(imagePath);

        if (!allowedExtensions.Contains(extension.ToLower()))
        {
            return BadRequest("File type not allowed");
        }
        
        var contentTypeProvider = new FileExtensionContentTypeProvider();
        
        var fullPath = Path.Combine(filesStorageService.UploadsPath, imagePath);

        if (!System.IO.File.Exists(fullPath))
        {
            return NotFound();
        }

        if (!contentTypeProvider.TryGetContentType(fullPath, out var contentType))
        {
            contentType = "application/octet-stream";
        }
        
        var file = PhysicalFile(fullPath, contentType);
        
        return Ok(file);
    }
}