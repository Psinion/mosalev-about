using Microsoft.AspNetCore.Http;
using MOS.Domain.Entities.Files;

namespace MOS.Application.Data.Services.Files;

public interface IFilesStorageService
{
    string UploadsPath { get; init; }
    
    Task<UploadedFile> SaveFileAsync(IFormFile file);
    Task<UploadedFile> DeleteFileAsync(string filePath);
    bool ValidateFile(IFormFile file);
}