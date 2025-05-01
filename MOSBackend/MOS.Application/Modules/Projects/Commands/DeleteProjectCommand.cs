using MOS.Application.Data;
using MOS.Application.OperationResults;

namespace MOS.Application.Modules.Projects.Commands;

public record DeleteProjectCommand : ICommand<OperationResult<bool>>
{
    public int Id { get; init; }

    public DeleteProjectCommand(int id)
    {
        Id = id;
    }
}