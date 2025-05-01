using System.Net;
using MOS.Application.Data.Repositories.Users;
using MOS.Application.Data.Services.Users;
using MOS.Application.OperationResults;
using MOS.Domain.Entities.Users;
using MOS.Domain.Enums;

namespace MOS.Identity.Services;

public class CredentialsService : ICredentialsService
{
    private readonly IUsersRepository usersRepository;
    private User? currentUser = null;

    public User? CurrentUser => currentUser;
    
    private Locale currentLocale = Locale.Ru;
    public Locale CurrentLocale
    {
        get => currentLocale;
        set => currentLocale = value;
    }
    
    public CredentialsService(IUsersRepository usersRepository)
    {
        this.usersRepository = usersRepository;
    }
    
    public async Task<OperationResult<User>> GetUserByIdAsync(int userId)
    {
        var user = await usersRepository.GetByIdAsync(userId);
        
        if (user == null)
        {
            return OperationError.NotFound("Such user not found");
        }

        return user;
    }
    
    public async Task<OperationResult<User>> InitUserAsync(int userId)
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