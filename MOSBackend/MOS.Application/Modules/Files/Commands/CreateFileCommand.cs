using MOS.Application.Data;
using MOS.Application.DTOs.Files.Responses;
using MOS.Application.OperationResults;
using MOS.Domain.Entities.Files;

namespace MOS.Application.Modules.Files.Commands;

public record CreateFileCommand : ICommand<OperationResult<UploadedFileDto>>
{
    public UploadedFile UploadedFile { get; init; }

    public CreateFileCommand(UploadedFile uploadedFile)
    {
        UploadedFile = uploadedFile;
    }
}