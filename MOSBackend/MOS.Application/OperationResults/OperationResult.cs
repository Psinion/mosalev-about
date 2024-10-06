using MOS.Application.OperationResults.Base;

namespace MOS.Application.OperationResults;

public struct OperationResult : IOperationResult
{
    public bool IsSuccess { get; }
    public IOperationError Error { get; }

    public OperationResult()
    {
        IsSuccess = true;
        Error = OperationError.None;
    }

    public OperationResult(IOperationError error)
    {
        IsSuccess = false;
        Error = error;
    }

    public static implicit operator bool(OperationResult result) 
        => result.IsSuccess;
}

public struct OperationResult<T> : IOperationResult
{
    public T? Value { get; }
    public bool IsSuccess { get; }
    public IOperationError Error { get; }

    public OperationResult(T value)
    {
        Value = value;
        IsSuccess = true;
        Error = OperationError.None;
    }

    public OperationResult(IOperationError error)
    {
        Value = default;
        IsSuccess = false;
        Error = error;
    }

    public static implicit operator OperationResult<T>(T value)
        => new(value);
    
    public static implicit operator OperationResult<T>(OperationError error)
        => new(error);

    public static implicit operator bool(OperationResult<T> result)
        => result.IsSuccess;
}