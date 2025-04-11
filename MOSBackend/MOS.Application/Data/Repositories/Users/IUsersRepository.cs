﻿using MOS.Domain.Entities.Users;

namespace MOS.Application.Data.Repositories.Users;

public interface IUsersRepository : IGenericRepository<User, long>
{
    Task<User?> GetByCredentialsAsync(string userName, string password);

    Task<List<User>> FilterByFioAsync(string? filter);
}