namespace MOS.Application.OperationResults.Base;

public interface IOperationResult
{
    bool IsSuccess { get; }
    IOperationError Error { get; }
}