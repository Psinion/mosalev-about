using MOS.Application.Data;
using MOS.Application.OperationResults;

namespace MOS.Application.Modules.Projects.Commands;

public record ChangeArticleProjectCommand : ICommand<OperationResult<bool>>
{
    public int Id { get; init; }
    public int? ProjectId { get; init; }
}