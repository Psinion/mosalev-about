using MOS.Application.Data.Services.Files;
using MOS.Domain.Entities.Files;
using MOS.Domain.Enums;
using MOS.WebApi.Extensions;

namespace MOS.WebApi.Services.Files;

public class FilesStorageService : IFilesStorageService
{
    private readonly string[] allowedMimeTypes = ["image/jpeg", "image/png", "image/gif"];
    
    public const int MaxFileSize = 10 * 1024 * 1024;
    public string UploadsPath { get; init; }

    public FilesStorageService(IWebHostEnvironment webHostEnvironment)
    {
        UploadsPath = webHostEnvironment.GetUploadsPath();
    }
    
    public async Task<UploadedFile> SaveFileAsync(IFormFile file)
    {
        var extension = Path.GetExtension(file.FileName);
        var storedFileName = $"{Guid.NewGuid()}{extension}";
        var yearMonth = DateTime.Now.ToString("yyyy.MM");
        var relativePath = Path.Combine(yearMonth, storedFileName);
        var absolutePath = Path.Combine(UploadsPath, relativePath);

        Directory.CreateDirectory(Path.GetDirectoryName(absolutePath)!);

        using var stream = new FileStream(absolutePath, FileMode.Create);
        await file.CopyToAsync(stream);

        FileKind fileKind;
        switch (file.ContentType)
        {
            case "image/jpeg":
            case "image/png":
            case "image/gif":
                fileKind = FileKind.Image;
                break;
            
            default:
                fileKind = FileKind.Other;
                break;
        }

        var fileToUpload = new UploadedFile()
        {
            OriginalName = file.FileName,
            StoredName = storedFileName,
            Size = file.Length,
            Kind = fileKind,
            Type = file.ContentType,
            Url = relativePath,
        };
        
        return fileToUpload;
    }

    public Task<UploadedFile> DeleteFileAsync(string filePath)
    {
        throw new NotImplementedException();
    }

    public bool ValidateFile(IFormFile file)
    {
        if (file.Length > MaxFileSize)
        {
            return false;
        }
        
        return allowedMimeTypes.Contains(file.ContentType);
    }
}