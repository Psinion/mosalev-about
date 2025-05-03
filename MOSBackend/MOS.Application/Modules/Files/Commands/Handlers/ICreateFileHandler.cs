using MOS.Application.Data;
using MOS.Application.DTOs.Files.Responses;
using MOS.Application.OperationResults;

namespace MOS.Application.Modules.Files.Commands.Handlers;

public interface ICreateFileHandler : ICommandHandler<CreateFileCommand, OperationResult<UploadedFileDto>>
{
}