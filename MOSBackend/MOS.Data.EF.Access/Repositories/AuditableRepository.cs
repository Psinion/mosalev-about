using Microsoft.EntityFrameworkCore;
using MOS.Application.Data.Repositories;
using MOS.Application.Data.Services.Users;
using MOS.Data.EF.Access.Contexts;
using MOS.Domain.Entities;

namespace MOS.Data.EF.Access.Repositories;

public class AuditableRepository<TEntity, TKey> : IAuditableRepository<TEntity, TKey> 
    where TEntity : class, IAuditableEntity<TKey>
    where TKey : struct
{
    protected readonly MainDbContext localContext;
    protected readonly DbSet<TEntity> localSet;
    protected readonly ICredentialsService credentialsService;

    public AuditableRepository(MainDbContext dbLocalContext, ICredentialsService credentialsService) {
        localContext = dbLocalContext;
        localSet = dbLocalContext.Set<TEntity>();
        this.credentialsService = credentialsService;
    }
    
    public virtual IQueryable<TEntity> GetAll() => localSet.AsNoTracking();
    
    public virtual async Task<bool> ExistsAsync(TKey id, CancellationToken cancellationToken = default) 
        => await localSet.AnyAsync(e => e.Id.Equals(id), cancellationToken);

    public virtual async Task<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default)
        => await localSet.FindAsync(new object[] { id }, cancellationToken);

    public virtual async Task<TEntity> CreateAsync(TEntity item, CancellationToken cancellationToken = default)
    {
        if (credentialsService.CurrentUser != null)
        {
            item.CreatedBy = credentialsService.CurrentUser.Id;
            item.CreatedAt = DateTime.UtcNow;
        }
        
        await localContext.AddAsync(item, cancellationToken);

        return item;
    }

    public virtual Task<TEntity> CreateOrUpdateAsync(TEntity item, CancellationToken cancellationToken = default)
    {
        var isNew = EqualityComparer<TKey>.Default.Equals(item.Id, default);
        
        var state = isNew ? EntityState.Added : EntityState.Modified;

        if (credentialsService.CurrentUser != null)
        {
            if (state == EntityState.Added)
            {
                item.CreatedBy = credentialsService.CurrentUser.Id;
                item.CreatedAt = DateTime.UtcNow;
            }
            else
            {
                item.UpdatedBy = credentialsService.CurrentUser.Id;
                item.UpdatedAt = DateTime.UtcNow;
            }
        }
        
        localContext.Entry(item).State = state;

        return Task.FromResult(item);
    }

    public virtual Task UpdateAsync(TEntity item, CancellationToken cancellationToken = default)
    {
        if (credentialsService.CurrentUser != null)
        {
            item.UpdatedBy = credentialsService.CurrentUser.Id;
            item.UpdatedAt = DateTime.UtcNow;
        }
        
        localContext.Update(item);
        return Task.CompletedTask;
    }
    
    public virtual Task DeleteAsync(TEntity item, CancellationToken cancellationToken = default)
    {
        if (credentialsService.CurrentUser != null)
        {
            item.DeletedBy = credentialsService.CurrentUser.Id;
            item.DeletedAt = DateTime.UtcNow;
            item.IsDeleted = true;
        }
        
        localContext.Update(item);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        localContext.Dispose();
    }
}