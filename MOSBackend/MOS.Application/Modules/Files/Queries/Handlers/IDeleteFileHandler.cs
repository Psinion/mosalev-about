using MOS.Application.Data;
using MOS.Application.OperationResults;

namespace MOS.Application.Modules.Files.Queries.Handlers;

/**
 * Return path to file
 */
public interface IDeleteFileHandler : ICommandHandler<DeleteFileCommand, OperationResult<string?>>
{
}