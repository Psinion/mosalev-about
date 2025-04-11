using Microsoft.EntityFrameworkCore;
using MOS.Application.Data.Repositories.Users;
using MOS.Data.EF.Access.Contexts;
using MOS.Domain.Entities.Users;

namespace MOS.Data.EF.Access.Repositories.Users;

public class UsersRepository : GenericRepository<User, long>, IUsersRepository
{
    public UsersRepository(MainDbContext dbLocalContext) : base(dbLocalContext)
    {
    }

    public async Task<User?> GetByCredentialsAsync(string userName, string password)
    {
        return await localSet.FirstOrDefaultAsync(user => user.UserName == userName && user.Password == password);
    }
    public async Task<List<User>> FilterByFioAsync(string? filter)
    {
        var items = localSet.AsQueryable();

        if (!string.IsNullOrEmpty(filter))
        {
            items = items.Where(user => (user.LastName + user.UserName + user.Patronymic).Contains(filter));
        }
        
        return await items
            .Select(x => new User()
            {
                Id = x.Id,
                UserName = x.UserName,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Patronymic = x.Patronymic,
            })
            .ToListAsync();
    }
}