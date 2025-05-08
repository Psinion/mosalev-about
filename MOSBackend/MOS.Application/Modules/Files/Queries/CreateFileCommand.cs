using MOS.Application.Data;
using MOS.Application.DTOs.Files.Responses;
using MOS.Application.OperationResults;

namespace MOS.Application.Modules.Files.Queries;

public record GetFileQuery : IQuery<OperationResult<UploadedFileDto>>
{
    public int FileId { get; init; }

    public GetFileQuery(int fileId)
    {
        FileId = fileId;
    }
}