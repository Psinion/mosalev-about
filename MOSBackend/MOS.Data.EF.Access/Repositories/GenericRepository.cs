using Microsoft.EntityFrameworkCore;
using MOS.Application.Data.Repositories;
using MOS.Data.EF.Access.Contexts;
using MOS.Domain.Entities;

namespace MOS.Data.EF.Access.Repositories;

public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> 
    where TEntity : class, IEntity<TKey>
    where TKey : struct
{
    protected readonly MainDbContext localContext;
    protected readonly DbSet<TEntity> localSet;

    public GenericRepository(MainDbContext dbLocalContext) {
        localContext = dbLocalContext;
        localSet = dbLocalContext.Set<TEntity>();
    }

    public virtual IQueryable<TEntity> GetAll() => localSet.AsQueryable().AsNoTracking();
    
    public virtual async Task<bool> ExistsAsync(TKey id, CancellationToken cancellationToken = default) 
        => await localSet.AnyAsync(e => e.Id.Equals(id), cancellationToken);

    public virtual async Task<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default)
        => await localSet.FindAsync(new object[] { id }, cancellationToken);

    public virtual async Task<TEntity> CreateAsync(TEntity item, CancellationToken cancellationToken = default)
    {
        await localContext.AddAsync(item, cancellationToken);

        return item;
    }

    public virtual Task<TEntity> CreateOrUpdateAsync(TEntity item, CancellationToken cancellationToken = default)
    {
        var isNew = EqualityComparer<TKey>.Default.Equals(item.Id, default);
        
        localContext.Entry(item).State = isNew
            ? EntityState.Added : EntityState.Modified;

        return Task.FromResult(item);
    }

    public virtual Task UpdateAsync(TEntity item, CancellationToken cancellationToken = default)
    {
        localContext.Update(item);
        return Task.CompletedTask;
    }

    public virtual async Task DeleteAsync(TKey id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity != null)
        {
            localSet.Remove(entity);
        }
    }

    public void Dispose()
    {
        localContext.Dispose();
    }
}