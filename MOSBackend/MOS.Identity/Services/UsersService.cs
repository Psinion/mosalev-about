using MOS.Application.Data.Repositories.Users;
using MOS.Application.Data.Services.Users;
using MOS.Application.DTOs.Users.Responses;
using MOS.Application.Mappings.Users;

namespace MOS.Identity.Services;

public class UsersService : IUsersService
{
    private readonly IUsersRepository usersRepository;
    
    public UsersService(IUsersRepository usersRepository)
    {
        this.usersRepository = usersRepository;
    }

    public async Task<List<UserResponseDto>> FilterByFioAsync(string? filter)
    {
        var users = await usersRepository.FilterByFioAsync(filter);
        return users.ToDtoList();
    }

    public void Dispose()
    {
        usersRepository.Dispose();
    }
}