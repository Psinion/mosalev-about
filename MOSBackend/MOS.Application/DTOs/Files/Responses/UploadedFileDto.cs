using MOS.Domain.Enums;

namespace MOS.Application.DTOs.Files.Responses;

public record UploadedFileDto
{
    public int Id { get; init; }
    public string OriginalName { get; init; } = string.Empty;
    public string StoredName { get; init; } = string.Empty;
    public string Url { get; init; } = string.Empty;
    
    public long Size { get; init; }
    public DateTime UploadDate { get; init; } = DateTime.UtcNow;

    public string Type { get; init; } = string.Empty;
    public FileKind Kind { get; init; } = FileKind.Other;
}