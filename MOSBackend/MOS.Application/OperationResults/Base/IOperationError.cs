using MOS.Application.OperationResults.Enums;

namespace MOS.Application.OperationResults.Base;

public interface IOperationError
{
    string Code { get; }
    string Description { get; }
    ErrorType ErrorType { get; }
}