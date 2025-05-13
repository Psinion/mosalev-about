using MOS.Application.Data;
using MOS.Application.OperationResults;

namespace MOS.Application.Modules.Files.Queries;

public record DeleteFileCommand : ICommand<OperationResult<string?>>
{
    public int Id { get; init; }

    public DeleteFileCommand(int id)
    {
        Id = id;
    }
}