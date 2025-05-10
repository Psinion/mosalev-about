using MOS.Application.Data;
using MOS.Application.DTOs.Files.Responses;
using MOS.Application.OperationResults;

namespace MOS.Application.Modules.Files.Queries;

public record GetFilesQuery : IQuery<OperationResult<UploadedFilesPaginationDto>>
{
    public int Limit { get; init; } = 0;
    public int Offset { get; init; } = 0;
}