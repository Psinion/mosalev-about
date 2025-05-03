using MOS.Domain.Enums;

namespace MOS.Application.DTOs.Files.Responses;

public record UploadedFileDto
{
    public int Id { get; init; }
    public string OriginalName { get; init; }
    public string StoredName { get; init; }
    public string Url { get; init; }
    
    public long Size { get; init; }
    public DateTime UploadDate { get; init; } = DateTime.UtcNow;

    public string Type { get; init; }
    public FileKind Kind { get; init; } = FileKind.Other;
}