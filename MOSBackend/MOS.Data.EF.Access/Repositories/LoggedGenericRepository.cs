using Microsoft.EntityFrameworkCore;
using MOS.Application.Data.Repositories;
using MOS.Application.Data.Services.Users;
using MOS.Data.EF.Access.Contexts;
using MOS.Domain.Entities;

namespace MOS.Data.EF.Access.Repositories;

public class LoggedGenericRepository<TEntity> : ILoggedRepository<TEntity> 
    where TEntity : class, ILoggedEntity, new()
{
    protected readonly MainDbContext LocalContext;
    protected readonly DbSet<TEntity> LocalSet;
    protected readonly ICredentialsService CredentialsService;

    public LoggedGenericRepository(MainDbContext dbLocalContext, ICredentialsService credentialsService) {
        LocalContext = dbLocalContext;
        LocalSet = dbLocalContext.Set<TEntity>();
        CredentialsService = credentialsService;
    }
    
    public virtual IQueryable<TEntity> GetAll() => LocalSet.AsQueryable();
    
    public virtual async Task<bool> ExistsAsync(long id, CancellationToken cancellationToken = default) 
        => await LocalSet.FindAsync(id) != null;

    public virtual async Task<TEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        => await LocalSet.FindAsync(new object[] { id }, cancellationToken);

    public virtual async Task<TEntity> CreateAsync(TEntity item, CancellationToken cancellationToken = default)
    {
        item.UserCreate = CredentialsService.CurrentUser;
        item.DateCreate = DateTime.UtcNow;
        item.UserUpdate = CredentialsService.CurrentUser;
        item.DateUpdate = DateTime.UtcNow;
        
        await LocalContext.AddAsync(item, cancellationToken);
        await LocalContext.SaveChangesAsync(cancellationToken);

        return item;
    }

    public virtual async Task<TEntity> CreateOrUpdateAsync(TEntity item, CancellationToken cancellationToken = default)
    {
        var state = item.Id == 0 ? EntityState.Added : EntityState.Modified;

        if (state == EntityState.Added)
        {
            item.UserCreate = CredentialsService.CurrentUser;
            item.DateCreate = DateTime.UtcNow;
            item.UserUpdate = CredentialsService.CurrentUser;
            item.DateUpdate = DateTime.UtcNow;
        }
        else
        {
            item.UserUpdate = CredentialsService.CurrentUser;
            item.DateUpdate = DateTime.UtcNow;
        }
        
        LocalContext.Entry(item).State = state;

        await LocalContext.SaveChangesAsync(cancellationToken);

        return item;
    }

    public virtual async Task UpdateAsync(TEntity item, CancellationToken cancellationToken = default)
    {
        item.UserUpdate = CredentialsService.CurrentUser;
        item.DateUpdate = DateTime.UtcNow;
        
        LocalContext.Update(item);
        await LocalContext.SaveChangesAsync(cancellationToken);
    }
    
    public virtual async Task DeleteAsync(TEntity item, CancellationToken cancellationToken = default)
    {
        item.UserDelete = CredentialsService.CurrentUser;
        item.DateDelete = DateTime.UtcNow;
        
        LocalContext.Update(item);
        await LocalContext.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        LocalContext.Dispose();
    }
}