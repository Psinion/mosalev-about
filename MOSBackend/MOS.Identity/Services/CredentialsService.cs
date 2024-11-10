using System.Net;
using MOS.Application.Data.Repositories.Users;
using MOS.Application.Data.Services.Users;
using MOS.Application.OperationResults;
using MOS.Domain.Entities.Users;

namespace MOS.Identity.Services;

public class CredentialsService : ICredentialsService
{
    private readonly IUsersRepository usersRepository;
    
    public CredentialsService(IUsersRepository usersRepository)
    {
        this.usersRepository = usersRepository;
    }
    
    public async Task<OperationResult<User>> GetUserByIdAsync(long userId)
    {
        var user = await usersRepository.GetByIdAsync(userId);
        
        if (user == null)
        {
            return OperationError.NotFound("not_found", "Such user not found");
        }

        return user;
    }
    
    public void Dispose()
    {
        usersRepository.Dispose();
    }
}