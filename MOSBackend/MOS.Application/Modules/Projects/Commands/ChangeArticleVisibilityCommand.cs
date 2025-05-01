using MOS.Application.Data;
using MOS.Application.OperationResults;

namespace MOS.Application.Modules.Projects.Commands;

public record ChangeArticleVisibilityCommand : ICommand<OperationResult<bool>>
{
    public int Id { get; init; }
    public bool Visible { get; init; }
}