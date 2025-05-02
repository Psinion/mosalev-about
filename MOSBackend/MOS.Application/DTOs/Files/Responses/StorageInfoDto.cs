namespace MOS.Application.DTOs.Files.Responses;

public record StorageInfoDto
{
    public long FreeSpace { get; init; }
    public long TotalSize { get; init; }
}