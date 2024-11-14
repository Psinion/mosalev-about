using MOS.Application.OperationResults;
using MOS.Domain.Entities.Users;

namespace MOS.Application.Data.Services.Users;

public interface ICredentialsService : IDisposable
{
    User? CurrentUser { get; }
    
    Task<OperationResult<User>> GetUserByIdAsync(long userId);
    Task<OperationResult<User>> InitUserAsync(long userId);
}