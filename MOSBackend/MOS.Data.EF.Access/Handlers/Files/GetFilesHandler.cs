using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MOS.Application.Data.DbAccesses;
using MOS.Application.DTOs.Files.Responses;
using MOS.Application.Extensions;
using MOS.Application.Modules.Files.Queries;
using MOS.Application.Modules.Files.Queries.Handlers;
using MOS.Application.OperationResults;
using MOS.Application.QueryObjects.Files;
using MOS.Data.EF.Access.Helpers;

namespace MOS.Data.EF.Access.Handlers.Files;

public class GetFilesHandler : IGetFilesHandler
{
    private readonly FileStorageSettings fileStorageSettings;
    private readonly IUploadedFilesDbAccess uploadedFilesDbAccess;

    public GetFilesHandler(IOptions<FileStorageSettings> fileStorageSettings, IUploadedFilesDbAccess uploadedFilesDbAccess)
    {
        this.fileStorageSettings = fileStorageSettings.Value;
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
            .MapUploadedFileToDto(fileStorageSettings.BaseUrl)
            .ToListAsync(cancellationToken);
        
        return new UploadedFilesPaginationDto()
        {
            Items = files,
            TotalCount = totalCount,
        };
    }
}