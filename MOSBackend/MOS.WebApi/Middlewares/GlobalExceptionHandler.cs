using Microsoft.AspNetCore.Diagnostics;
using MOS.Application.OperationResults;

namespace MOS.WebApi.Middlewares;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.ContentType = "application/json";
        
        await httpContext.Response.WriteAsJsonAsync(CreateProblemDetails(httpContext, exception), cancellationToken: cancellationToken);
        return true;
    }
    
    private OperationError CreateProblemDetails(
        HttpContext context, 
        Exception exception)
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        return OperationError.ServerError(description: 
#if DEBUG
            exception.ToString()
#else
                string.Empty
#endif
            );
    }
}