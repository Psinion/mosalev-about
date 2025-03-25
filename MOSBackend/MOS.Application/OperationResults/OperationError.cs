using MOS.Application.OperationResults.Base;
using MOS.Application.OperationResults.Enums;

namespace MOS.Application.OperationResults;

public sealed record OperationError(string Type, string Code, int CodeNumber, string Description, ErrorType ErrorType) : IOperationError
{
    public static readonly OperationError None = new( string.Empty, string.Empty, -1, string.Empty, ErrorType.Failure);
    
    public static OperationError Failure(string code = "fail", string description = "") =>
        new("https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1", code, 400, description, ErrorType.Failure);
    
    public static OperationError NotFound(string code = "not_found", string description = "") =>
        new("https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4", code, 404, description, ErrorType.NotFound);

    public static OperationError Unauthorized(string code = "unauthorized", string description = "") =>
        new("https://datatracker.ietf.org/doc/html/rfc7235#section-3.1", code, 401, description, ErrorType.Unauthorized);
    
    public static OperationError ServerError(string code = "server_error", string description = "") =>
        new("https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1", code, 500, description, ErrorType.ServerError);
}