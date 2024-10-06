using MOS.Domain.Entities.Users;

namespace MOS.Application.Data.Repositories.Users;

public interface IUsersRepository : IGenericRepository<User>
{
    Task<User?> GetByCredentialsAsync(string userName, string password);
}