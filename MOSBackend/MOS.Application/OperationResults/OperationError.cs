using MOS.Application.OperationResults.Base;
using MOS.Application.OperationResults.Enums;

namespace MOS.Application.OperationResults;

public sealed record OperationError(string Code, string Description, ErrorType ErrorType) : IOperationError
{
    public static readonly OperationError None = new(string.Empty, string.Empty, ErrorType.Failure);
    
    public static OperationError Failure(string code = "fail", string description = "") =>
        new(code, description, ErrorType.Failure);
    
    public static OperationError NotFound(string code = "not_found", string description = "") =>
        new(code, description, ErrorType.NotFound);    
    
    public static OperationError Unauthorized(string code = "unauthorized", string description = "") =>
        new(code, description, ErrorType.Unauthorized);
}