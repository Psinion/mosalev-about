using MOS.Application.Data.Repositories;
using MOS.Domain.Entities.Users;

namespace MOS.Application.Data.Users;

public interface IUsersRepository : IGenericRepository<User>
{
    Task<User?> GetByCredentialsAsync(string userName, string password);
}