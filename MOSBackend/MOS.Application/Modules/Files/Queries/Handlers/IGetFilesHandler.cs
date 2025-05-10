using MOS.Application.Data;
using MOS.Application.DTOs.Files.Responses;
using MOS.Application.OperationResults;

namespace MOS.Application.Modules.Files.Queries.Handlers;

public interface IGetFilesHandler : IQueryHandler<GetFilesQuery, OperationResult<UploadedFilesPaginationDto>>
{
}