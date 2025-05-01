using MOS.Application.Data;
using MOS.Application.OperationResults;

namespace MOS.Application.Modules.Projects.Commands;

public record ChangeProjectVisibilityCommand : ICommand<OperationResult<bool>>
{
    public long Id { get; init; }
    public bool Visible { get; init; }
}