using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MOS.Application.Data.DbAccesses;
using MOS.Application.DTOs.Files.Responses;
using MOS.Application.DTOs.Projects.Responses;
using MOS.Application.Modules.Files.Queries;
using MOS.Application.Modules.Files.Queries.Handlers;
using MOS.Application.OperationResults;
using MOS.Application.QueryObjects.Files;
using MOS.Application.QueryObjects.Projects;
using MOS.Data.EF.Access.Helpers;

namespace MOS.Data.EF.Access.Handlers.Files;

public class GetFileHandler : IGetFileHandler
{
    private readonly FileStorageSettings fileStorageSettings;
    private readonly IUploadedFilesDbAccess uploadedFilesDbAccess;
    
    public GetFileHandler(IOptions<FileStorageSettings> fileStorageSettings, IUploadedFilesDbAccess uploadedFilesDbAccess)
    {
        this.fileStorageSettings = fileStorageSettings.Value;
        this.uploadedFilesDbAccess = uploadedFilesDbAccess;
    }
    
    public async Task<OperationResult<UploadedFileDto>> Handle(GetFileQuery request, CancellationToken cancellationToken = default)
    {
        var file = await uploadedFilesDbAccess.GetUploadedFiles()
            .MapUploadedFileToDto(fileStorageSettings.BaseUrl)
            .FirstOrDefaultAsync(x => x.Id == request.FileId, cancellationToken);
        
        if (file == null)
        {
            return OperationError.NotFound("Not found");
        }

        return file;
    }
}