using MOS.Application.Data;
using MOS.Application.OperationResults;

namespace MOS.Application.Modules.Projects.Commands.Handlers;

public interface IChangeProjectVisibilityHandler : ICommandHandler<ChangeProjectVisibilityCommand, OperationResult<bool>>
{
    
}