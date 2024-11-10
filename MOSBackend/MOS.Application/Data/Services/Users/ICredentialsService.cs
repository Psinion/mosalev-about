using MOS.Application.OperationResults;
using MOS.Domain.Entities.Users;

namespace MOS.Application.Data.Services.Users;

public interface ICredentialsService : IDisposable
{
    Task<OperationResult<User>> GetUserByIdAsync(long userId);
}