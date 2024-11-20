using System.Net;
using MOS.Application.Data.Repositories.Users;
using MOS.Application.Data.Services.Users;
using MOS.Application.OperationResults;
using MOS.Domain.Entities.Users;

namespace MOS.Identity.Services;

public class CredentialsService : ICredentialsService
{
    private readonly IUsersRepository usersRepository;
    private User? currentUser = null;

    public User? CurrentUser => currentUser;
    
    public CredentialsService(IUsersRepository usersRepository)
    {
        this.usersRepository = usersRepository;
    }
    
    public async Task<OperationResult<User>> GetUserByIdAsync(long userId)
    {
        var user = await usersRepository.GetByIdAsync(userId);
        
        if (user == null)
        {
            return OperationError.NotFound("Such user not found");
        }

        return user;
    }
    
    public async Task<OperationResult<User>> InitUserAsync(long userId)
    {
        var user = await usersRepository.GetByIdAsync(userId);
        
        if (user == null)
        {
            return OperationError.NotFound("Such user not found");
        }

        currentUser = user;
        return user;
    }
    
    public void Dispose()
    {
        usersRepository.Dispose();
    }
}