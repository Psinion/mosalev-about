using MOS.Application.OperationResults.Enums;

namespace MOS.Application.OperationResults.Base;

public interface IOperationError
{
    string Type { get; }
    string Code { get; }
    int CodeNumber { get; }
    string Description { get; }
    ErrorType ErrorType { get; }
}