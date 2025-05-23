using MOS.Application.DTOs.Files.Responses;
using MOS.Domain.Entities.Files;

namespace MOS.Application.QueryObjects.Files;

public static class FilesQueryObjects
{
    public static IQueryable<UploadedFileDto> MapUploadedFileToDto(this IQueryable<UploadedFile> files, string baseUrl)    
    {
        return files.Select(entity => new UploadedFileDto()
        {
            Id = entity.Id,
            OriginalName = entity.OriginalName,
            StoredName = entity.StoredName,
            Kind = entity.Kind,
            Size = entity.Size,
            Type = entity.Type,
            UploadDate = entity.UploadDate,
            Url = $"{baseUrl}/{entity.Url}"
        });
    }
}