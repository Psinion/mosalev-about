using Microsoft.EntityFrameworkCore;
using MOS.Application.Data.DbAccesses;
using MOS.Application.DTOs.Files.Responses;
using MOS.Application.Extensions;
using MOS.Application.Modules.Files.Queries;
using MOS.Application.Modules.Files.Queries.Handlers;
using MOS.Application.OperationResults;
using MOS.Application.QueryObjects.Files;

namespace MOS.Data.EF.Access.Handlers.Files;

public class GetFilesHandler : IGetFilesHandler
{
    private readonly IUploadedFilesDbAccess uploadedFilesDbAccess;

    public GetFilesHandler(IUploadedFilesDbAccess uploadedFilesDbAccess)
    {
        this.uploadedFilesDbAccess = uploadedFilesDbAccess;
    }
    public async Task<OperationResult<UploadedFilesPaginationDto>> Handle(GetFilesQuery request, CancellationToken cancellationToken = default)
    {
        var query = uploadedFilesDbAccess
                .GetUploadedFiles()
            ;
        
        var totalCount = await query.CountAsync(cancellationToken);
        var files = await query
            .OrderByDescending(x => x.UploadDate)
            .Page(request.Limit, request.Offset)
            .MapUploadedFileToDto()
            .ToListAsync(cancellationToken);
        
        return new UploadedFilesPaginationDto()
        {
            Items = files,
            TotalCount = totalCount,
        };
    }
}