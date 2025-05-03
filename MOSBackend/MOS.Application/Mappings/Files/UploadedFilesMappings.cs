using MOS.Application.DTOs.Files.Responses;
using MOS.Domain.Entities.Files;

namespace MOS.Application.Mappings.Files;

public static class UploadedFilesMappings
{
    public static UploadedFileDto ToDto(this UploadedFile entity)
    {
        return new UploadedFileDto()
        {
            Id = entity.Id,
            OriginalName = entity.OriginalName,
            StoredName = entity.StoredName,
            Kind = entity.Kind,
            Size = entity.Size,
            Type = entity.Type,
            UploadDate = entity.UploadDate,
            Url = entity.Url
        };
    }
}