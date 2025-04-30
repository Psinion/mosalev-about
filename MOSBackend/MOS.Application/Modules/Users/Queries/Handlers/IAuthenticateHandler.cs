using MOS.Application.Data;
using MOS.Application.DTOs.Users.Responses;
using MOS.Application.OperationResults;

namespace MOS.Application.Modules.Users.Queries.Handlers;

public interface IAuthenticateHandler : IQueryHandler<AuthenticateQuery, OperationResult<AuthenticateResponseDto>>
{
}