﻿using MOS.Application.OperationResults;
using MOS.Domain.Entities.Users;
using MOS.Domain.Enums;

namespace MOS.Application.Data.Services.Users;

public interface ICredentialsService : IDisposable
{
    User? CurrentUser { get; }
    Locale CurrentLocale { get; set; }
    
    Task<OperationResult<User>> GetUserByIdAsync(int userId);
    Task<OperationResult<User>> InitUserAsync(int userId);
}