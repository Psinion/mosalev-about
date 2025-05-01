using MOS.Application.Data;
using MOS.Application.OperationResults;

namespace MOS.Application.Modules.Projects.Commands;

public record DeleteProjectCommand : ICommand<OperationResult<bool>>
{
    public long Id { get; init; }

    public DeleteProjectCommand(long id)
    {
        Id = id;
    }
}