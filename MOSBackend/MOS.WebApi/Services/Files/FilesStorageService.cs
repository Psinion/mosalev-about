using MOS.Application.Data.Services.Files;
using MOS.Domain.Entities.Files;
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
        var fullPath = Path.Combine(UploadsPath, yearMonth, storedFileName);

        Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);

        using var stream = new FileStream(fullPath, FileMode.Create);
        await file.CopyToAsync(stream);

        var fileToUpload = new UploadedFile()
        {
            OriginalName = file.FileName,
            StoredName = storedFileName,
            Size = file.Length,
            Type = extension,
            Url = fullPath,
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