using MOS.Application.DTOs.Users.Requests;
using MOS.Application.DTOs.Users.Responses;
using MOS.Application.OperationResults;
using MOS.Domain.Entities.Users;

namespace MOS.Application.Data.Services.Users;

public interface IUsersService : IDisposable
{
    Task<OperationResult<AuthenticateResponseDto>> AuthenticateAsync(AuthenticateRequestDto authenticateRequest);
    
    Task<List<UserResponseDto>> FilterByFioAsync(string? filter);
}