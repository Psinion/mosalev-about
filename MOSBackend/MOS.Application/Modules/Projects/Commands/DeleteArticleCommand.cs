using MOS.Application.Data;
using MOS.Application.OperationResults;

namespace MOS.Application.Modules.Projects.Commands;

public record DeleteArticleCommand : ICommand<OperationResult<bool>>
{
    public int Id { get; init; }

    public DeleteArticleCommand(int id)
    {
        Id = id;
    }
}