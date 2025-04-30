using MOS.Application.DTOs.Users.Responses;
using MOS.Application.OperationResults;
using MOS.Identity.Queries;

namespace MOS.Application.Data.Handlers;

public interface IAuthenticateHandler : IRequestHandler<AuthenticateQuery, OperationResult<AuthenticateResponseDto>>
{
    
}