using Microsoft.EntityFrameworkCore;
using MOS.Application.Data.Repositories.Users;
using MOS.Data.EF.Access.Contexts;
using MOS.Domain.Entities.Users;

namespace MOS.Data.EF.Access.Repositories.Users;

public class UsersRepository : GenericRepository<User>, IUsersRepository
{
    public UsersRepository(MainDbContext dbLocalContext) : base(dbLocalContext)
    {
    }

    public async Task<User?> GetByCredentialsAsync(string userName, string password)
    {
        return await LocalSet.FirstOrDefaultAsync(user => user.UserName == userName && user.Password == password);
    }
}