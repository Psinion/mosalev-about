using MOS.Application.DTOs.Users.Responses;

namespace MOS.Application.Data.Services.Users;

public interface IUsersService : IDisposable
{
    Task<List<UserResponseDto>> FilterByFioAsync(string? filter);
}